using AngleSharp.Html.Dom;
using AngleSharp;
using System.Diagnostics;
using AngleSharp.Dom;
using System.Drawing;
using AngleSharp.Io;

namespace FreeGSL
{
    public class GetUpdate
    {
        public async Task<IDocument> GettingUpdate(string package)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var requester = context.GetService<DefaultHttpRequester>();
            requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            // 検索ページを開く

            var doc = await context.OpenAsync($"https://google.co.jp");
            var form = doc.Forms.FirstOrDefault();
            var result = await form.SubmitAsync(new { q = $"site:forest.watch.impress.co.jp {package}" });
            return result;
        }

    }
}
