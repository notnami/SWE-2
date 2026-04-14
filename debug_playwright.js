const { chromium } = require('playwright');
(async () => {
  const browser = await chromium.launch();
  const page = await browser.newPage();
  page.on('console', msg => console.log('CONSOLE:', msg.text()));
  page.on('pageerror', err => console.log('PAGEERROR:', err.toString()));
  await page.goto('http://localhost:5161/snacks.html');
  await page.fill('#searchInput', 'apple');
  await page.click('.search-btn');
  await page.waitForSelector('#snackList li:has(.star)', { timeout: 20000 });
  const count = await page.locator('#snackList li:has(.star)').count();
  console.log('COUNT:', count);
  if (count > 0) {
    console.log('FIRST:', await page.locator('#snackList li:has(.star)').first.innerHTML());
  }
  const content = await page.innerHTML('#snackList');
  console.log('SNACKLIST:', content);
  await browser.close();
})();
