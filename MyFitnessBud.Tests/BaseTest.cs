using Microsoft.Playwright;
using Xunit;

namespace MyFitnessBud.Tests
{
    public abstract class BaseTest : IAsyncLifetime
    {
        protected IPage page = null!;
        protected IBrowserContext context = null!;
        protected IBrowser browser = null!;
        protected IPlaywright playwright = null!;
        protected string? LastDialogMessage = null;
        
        private const string BaseUrl = "http://localhost:5161/";
        private const int TimeoutMs = 10000; // 10 seconds

        public virtual async Task InitializeAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = true
            });
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
            page.SetDefaultTimeout(TimeoutMs);
            page.SetDefaultNavigationTimeout(TimeoutMs);
            
            // Handle all dialogs automatically
            page.Dialog += async (_, dialog) =>
            {
                LastDialogMessage = dialog.Message;
                await dialog.AcceptAsync();
            };
            
            await page.GotoAsync(BaseUrl);
        }

        public virtual async Task DisposeAsync()
        {
            if (page != null)
                await page.CloseAsync();
            if (context != null)
                await context.CloseAsync();
            if (browser != null)
                await browser.CloseAsync();
            if (playwright != null)
                playwright.Dispose();
        }

        protected async Task WaitForElement(string selector, int timeoutMs = 10000)
        {
            await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions { Timeout = timeoutMs });
        }

        protected async Task<ILocator> FindElement(string selector)
        {
            return page.Locator(selector);
        }

        protected async Task Click(string selector)
        {
            await page.ClickAsync(selector);
        }

        protected async Task Fill(string selector, string text)
        {
            await page.FillAsync(selector, text);
        }

        protected async Task<string> GetText(string selector)
        {
            return await page.TextContentAsync(selector) ?? "";
        }

        protected async Task<string> GetInputValue(string selector)
        {
            return await page.InputValueAsync(selector);
        }

        protected async Task ExecuteScript(string script, object[] args)
        {
            await page.EvaluateAsync(script, args);
        }

        protected async Task<object?> EvaluateScript(string script, object[] args)
        {
            return await page.EvaluateAsync(script, args);
        }

        protected async Task GoToUrl(string url)
        {
            await page.GotoAsync(url);
        }

        protected async Task<string> GetCurrentUrl()
        {
            return page.Url;
        }

        protected async Task RefreshPage()
        {
            await page.ReloadAsync();
        }

        protected async Task<string?> WaitForDialog(Func<Task> action)
        {
            LastDialogMessage = null;
            await action();
            // Give dialog time to fire
            await Task.Delay(100);
            return LastDialogMessage;
        }
    }
}

