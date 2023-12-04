using ABI.System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace FreeGSL
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        [Obsolete]
        public MainPage()
        {
            InitializeComponent();
            Load();
            Debug.Write("(´・ω・`)");
            Debug.WriteLine("起動完了");
        }

        int Runtime;
        int EU;
        int update;
        bool import = false;

        private void CINI()
        {
            if (!File.Exists(@"C:\FreeGSL\Settings.ini"))
            {

            }
            else
            {
                string[] set = new string[3];


                using (StreamReader sr = new StreamReader(@"C:\FreeGSL\Settings.ini"))
                {
                    int i = 0;
                    // 読み込みできる文字がなくなるまで繰り返す
                    while (sr.Peek() >= 0)
                    {
                        // ファイルを 1 行ずつ読み込む
                        string stBuffer = sr.ReadLine();
                        // 読み込んだものを追加で格納する
                        set[i] = stBuffer;
                        i++;
                    }
                }
                update = Convert.ToInt32(set[0].Split(',')[1]);
                Runtime = Convert.ToInt32(set[1].Split(',')[1]);
                EU = Convert.ToInt32(set[2].Split(",")[1]);

            }

        }
        /// <summary>
        /// インストールされているプログラムを取得
        /// </summary>

        public class Info_App
        {
            public string Name { get; set; } = "";
            public string Ver { get; set; } = "";
            public string Url { get; set; } = "";
        }
        /// <summary>
        /// アップデートを取得するかどうか尋ねる（未実装）
        /// </summary>
        public async void GetUpdateAppData()
        {
            /*
            HttpClient httpClient = new HttpClient();
            string get = await httpClient.GetStringAsync("https://hoge.net/");
            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(get);

            if (get == "Yes")
            {
                //更新するかどうか聞く
            }*/

        }
        /// <summary>
        /// ランタイム系を除外する
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>

        public ObservableCollection<Info_App> Info = new ObservableCollection<Info_App>();
        public ObservableCollection<Info_App> Search_Info = new ObservableCollection<Info_App>();

        private void button_Clicked(object sender, EventArgs e)
        {

            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                if (SearchBar_Bar.Text == null)
                {
                    if (File.Exists($@"{folder}\ProgramList.FGSL"))
                    {
                        var Que = MessageBox.Show("プログラムリストを上書きしてもいいですか？", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Que != DialogResult.Yes)
                        {
                            MessageBox.Show("キャンセルしました", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            File.Delete($@"{folder}\ProgramList.FGSL");
                            Create(folder);
                        }
                    }
                    else
                    {
                        Create(folder);
                    }
                }
                else
                {
                    if (File.Exists($@"{folder}\ProgramList_{SearchBar_Bar.Text}.FGSL"))
                    {
                        var Que = MessageBox.Show("プログラムリストを上書きしてもいいですか？", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Que != DialogResult.Yes)
                        {
                            MessageBox.Show("キャンセルしました","Infomation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            File.Delete($@"{folder}\ProgramList_{SearchBar_Bar.Text}.FGSL");
                            Create(folder);
                        }
                    }
                    else
                    {
                        Create(folder);
                    }
                }
            }
            else
            {

            }
        }
        /// <summary>
        /// プログラムのリストを書き出す
        /// </summary>
        /// <param name="folder"></param>
        private async void Create(string folder)
        {
            StreamWriter sw;
            DateTime dt = DateTime.Now;
            string machine = Environment.MachineName;
            string user = Environment.UserName;
            if (SearchBar_Bar.Text == String.Empty || SearchBar_Bar.Text == null)
            {
                try
                {
                    sw = new StreamWriter($@"{folder}\ProgramList_{dt.ToString("yyyy年MM月dd日_HH時mm分")}_{machine}_{user}.FGSL", true);
                    foreach (var a in Info)
                    {
                        sw.WriteLine($"{a.Name},{a.Ver},{a.Url}");
                    }
                    sw.Close();
                    await DisplayAlert("Success", "書き出し完了！", "OK");
                }
                catch (System.Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "OK");
                }
            }
            else
            {
                try
                {
                    sw = new StreamWriter($@"{folder}\ProgramList_{SearchBar_Bar.Text}_{dt.ToString("yyyy年MM月dd日_HH時mm分")}_{machine}_{user}.FGSL", true);
                    foreach (var a in Search_Info)
                    {
                        sw.WriteLine($"{a.Name},{a.Ver},{a.Url}");
                    }
                    sw.Close();
                    await DisplayAlert("Success", "書き出し完了！", "OK");
                }
                catch (System.Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "OK");
                }
            }


        }



        /// <summary>
        /// 起動時にリストを作成する
        /// </summary>
        ///
        [Obsolete]
        private async void Load()
        {
            loading.IsRunning = true;
            programList.ItemsSource = "";
            GetList gl = new GetList();
            var uninstall_list = await gl.GetUninstallList();


            CINI();



            await Task.Yield();
            await Task.Run(() =>
            {
                GetList gl = new GetList();

                if (Runtime == 1)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Info = gl.RuntimeExclude(uninstall_list);
                        programList.ItemsSource = Info;
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Info = uninstall_list;
                        programList.ItemsSource = uninstall_list;
                    });
                }
            });
            loading.IsRunning = false;
        }

        /// <summary>
        /// ここでセットアップファイルを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        private void programList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection[0] != null)
            {
                var itemSelected = e.CurrentSelection[0] as Info_App;
                GetUpdate gu = new GetUpdate();
                Winget winget = new Winget();

                if (itemSelected != null)
                {
                    string Url = (e.CurrentSelection.FirstOrDefault() as Info_App)?.Url;
                    string Name = (e.CurrentSelection.FirstOrDefault() as Info_App)?.Name;

                    /*var a = await gu.GettingUpdate(Name);
                    Debug.WriteLine(a.ToHtml());
                    var titles = a.QuerySelectorAll("h3").Select(x => x.TextContent);
                    var i = 1;
                    foreach (var item in titles)
                    {
                        Debug.WriteLine($"{i++:00}: {item}");
                        MessageBox.Show(item);
                    }
                    */
                    // winget.get(Name);
                    programList.SelectedItem = SelectableItemsView.EmptyViewProperty;
                    ProcessStartInfo pi = new ProcessStartInfo()
                    {
                        FileName = ($"https://www.google.com/search?q={Name}" + "+" + "Download"),
                        UseShellExecute = true,
                    };
                    Process.Start(pi);
                }
            }
          
        }
        [Obsolete]
        private async void SearchBar_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {

            loading.IsRunning = true;
            if (SearchBar_Bar.Text == "")
            {
                await Task.Yield();
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (programList.ItemsSource != null)
                            programList.ItemsSource = "";
                        programList.ItemsSource = Info;
                    });
                });
            }
            Search_Info.Clear();
            loading.IsRunning = false;

            // programList.ItemsSource = 
        }

        [Obsolete]
        private async void SearchBar_Bar_SearchButtonPressed(object sender, EventArgs e)
        {
            loading.IsRunning = true;
            Search_Info.Clear();
            foreach (var item in Info)
            {
                var a = (item as Info_App)?.Name.Contains(SearchBar_Bar.Text,StringComparison.OrdinalIgnoreCase);
                Debug.WriteLine((item as Info_App)?.Name);
                if ((bool)a)
                {

                    Search_Info.Add(new Info_App { Name = (item as Info_App)?.Name, Ver = (item as Info_App)?.Ver, Url = (item as Info_App)?.Url });

                }

            }
            if (Search_Info.Count == 0)
            {
                await DisplayAlert("Error", "一致する項目がありませんでした", "OK");
                SearchBar_Bar.Text = String.Empty;
                Load();
                Title = "Free Program List Extractor（インストール済みプログラム抽出ソフト）";
                Debug.WriteLine("(´・ω・｀)再読み込み終了");
            }
            else
            {
                programList.ItemsSource = "";
                GC.Collect();
                programList.ItemsSource = Search_Info;
            }
            loading.IsRunning = false;

        }

        [Obsolete]
        private async void button_Clicked_1(object sender, EventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.DefaultExtension = "FGSL";
            dlg.Title = "FGSLファイルを選択してください";
            ObservableCollection<Info_App> im = new ObservableCollection<Info_App>();
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                loading.IsRunning = true;
                try
                {
                    await set(dlg);
                    Title = "読み込み完了 " + $"{Path.GetFileNameWithoutExtension(dlg.FileName)}";
                    loading.IsRunning = false;
                }catch(System.Exception ex)
                {
                    await DisplayAlert("Error", "FGSLファイルではないものを読み込んでいませんか？\nそれ以外でしたらお問い合わせください。", "OK");
                    Load();
                }

            }
        }

        private async Task set(CommonOpenFileDialog dlg)
        {
            GetList getList = new GetList();

            var result = await getList.Getimport(dlg.FileName);
            programList.ItemsSource = "";
            programList.ItemsSource = result;
        }

        [Obsolete]
        private void button_2_Clicked(object sender, EventArgs e)
        {
            Load();
            Title = "Free Program List Extractor（インストール済みプログラム抽出ソフト）";
        }
    }
}
/*
The MIT License (MIT)

Copyright (c) 2013 - 2023 AngleSharp

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

HtmlAgilityPack

The MIT License (MIT)
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
