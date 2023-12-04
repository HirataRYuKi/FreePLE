using System.Diagnostics;

namespace FreeGSL;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
        CINI();
    }

    private int u = 0;

    /// <summary>
    /// iniファイルを作成（ini形式ではない）
    /// </summary>
    private void CINI()
    {
        if (!File.Exists(@"C:\FreeGSL\Settings.ini"))
        {
            Directory.CreateDirectory(@"C:\FreeGSL");
            using (File.Create(@"C:\FreeGSL\Settings.ini")) ;

        }
        else
        {
            string[] Setting = new string[3];
            string[] Runtime;
            string[] EU;
            string[] update;

            using (StreamReader sr = new StreamReader(@"C:\FreeGSL\Settings.ini"))
            {
                int i = 0;
                // 読み込みできる文字がなくなるまで繰り返す
                while (sr.Peek() >= 0)
                {
                    // ファイルを 1 行ずつ読み込む
                    string stBuffer = sr.ReadLine();
                    // 読み込んだものを追加で格納する
                    Setting[i] = stBuffer;
                    i++;
                }
            }
            update = Setting[0].Split(',');
            Runtime = Setting[1].Split(',');
            EU = Setting[2].Split(","); 
            if (update[1] == "1")
            {
                Update.IsChecked = true;
            }
            else
            {
                Update.IsChecked = false;
            }

            if (Runtime[1] == "1")
            {
                runtime.IsChecked = true;
            }
            else
            {
                runtime.IsChecked = false;
            }

            if (EU[1] == "1")
            {
                Enable_Update.IsChecked = true;
            }
            else
            {
                Enable_Update.IsChecked = false;
            }

        }

    }
    /// <summary>
    /// アップデートを有効にするか切り替えできる
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Check_Update(object sender, CheckedChangedEventArgs e)
    {


    }
    private void Check_Source(object sender, CheckedChangedEventArgs e)
    {

    }
    private void Disable_Runtime(object sender, CheckedChangedEventArgs e)
    {

    }

    private void Enable_Update_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void Help_Clicked(object sender, EventArgs e)
    {
        ProcessStartInfo pi = new ProcessStartInfo()
        {
            FileName = "mailto:kd1319807@st.kobedenshi.ac.jp?subject=FreePLEについてのお問い合わせ&body=ここにお問い合わせ内容をお書きください。",
            UseShellExecute = true
        };
        Process.Start(pi);
    }
    /// <summary>
    /// まとめて設定を書き出す
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        Save();
    }

    private void Save()
    {
        string all = $"Update,{(Update.IsChecked ? "1" : "0")}\n" + $"runtime,{(runtime.IsChecked ? "1" : "0")}\n" + $"Enable_Update,{(Enable_Update.IsChecked ? "1" : "0")}";
        using (StreamWriter sw = new StreamWriter(@"C:\FreeGSL\Settings.ini"))
        {
            sw.WriteLine(all);
        }
    }
    private void ContentPage_Unfocused(object sender, FocusEventArgs e)
    {
        Save();
        /*
     * まとめて書き出す*/
    }
}

/*ワァ...
/*
　　　　　　/⌒Y_ -　 ￣ ｀ -/⌒Y
　　　　　　ゝ　　　　　　　　　 　 '｀ 、
　　　　 ／　　　　　　　　　　　　　　`､
　　　　′　　　　　 　 　 　 　 　 　 　 ﾞ:,
　　　/ 　 　 __ 　 　 __　　 　 　 　 　 　 ',
　 　 |　　　　　　　　　　　　　　　　　　　}
　　　', /:/: ⌒ 、、　⌒ /:/:/　　　　　　;
　　 　 、 　 ,.:'´:7^¨´ヽ　　　　　　　　 /
　 　 　 ＼__l:. : {:. :. :. :.}_　　　　　　　i´
　　　　　 (__}￣￣￣￣ }/¨　　　　　;
　　 　 　 　 }＿＿＿＿_}｀¨¨´　 　 /_)
　　　 　 { {　(__,､　　　､､　　_,,.､丶´
　　　　　ヽヽ　　｀ ｰ--‐(ヽノ、
*/


/* 2023/10/11
 * とりあえず、表面上の機能は考えた。.NET MAUIは癖が強い…
 * WPFアプリにしようか考えたが、チーム政策でも使うので慣れておきたいと思ったから.NET MAUIにした。
 */

/*
 * 「2位じゃダメなんでしょうか」
 * 富岳の見学に行ってふと思い出した*/

