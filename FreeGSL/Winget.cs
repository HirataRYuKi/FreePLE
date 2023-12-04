using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Windows.Management.Deployment.Preview;

namespace FreeGSL
{
    internal class Winget
    {
        /// <summary>
        /// パッケージの検索
        /// </summary>
        /// <param name="name"></param>
        public void get(string name)
        {
            string args= "";
            int i = 0;
            var si = new ProcessStartInfo();
            {
                si.FileName = $@"{Environment.GetEnvironmentVariable("windir")}\System32\WindowsPowerShell\v1.0\powershell.exe";
                si.ArgumentList.Add("winget");
                si.ArgumentList.Add("search");
                si.ArgumentList.Add($"\"{name}\"");
                //si.Arguments = ($"winget search" +" "+ "\"\"" + name + "\"\"");
                si.CreateNoWindow = true;
                si.RedirectStandardOutput = true;
                si.RedirectStandardError = true;
                si.UseShellExecute = false;
                si.StandardOutputEncoding = Encoding.UTF8; // エンコーディング設定
            };

            using (var proc = new Process())
            {
                proc.EnableRaisingEvents = true;
                proc.StartInfo = si;

                proc.OutputDataReceived += (sender, ev) =>
                {
                    if (ev.Data is not null)
                    {
                        Debug.WriteLine($"{ev.Data}");
                        if(ev.Data.Contains(name))
                        {
                            args += ev.Data +"\n";
                            i++;
                        }
                        
                    }
                    else
                        Debug.WriteLine($"標準出力 ev.Data がnull");
                };
                proc.ErrorDataReceived += (sender, ev) =>
                {
                    if (ev.Data is not null)
                        Debug.WriteLine($"標準エラー出力 {ev.Data}");
                    else
                        Debug.WriteLine($"標準エラー出力 ev.Data がnull");
                };
                proc.Exited += (sender, ev) =>
                {
                    Debug.WriteLine($"終了イベント到来");
                };

                // プロセス起動
                proc.Start();

                // 非同期出力読出し開始
                proc.BeginErrorReadLine();
                proc.BeginOutputReadLine();

                // 終了まで(同期的に)待つ
                proc.WaitForExit();
            }
            Debug.WriteLine("終了");
            /*if(args != null)
            {
                //ここの処理どうするか
                string id = args.Split(' ')[1];
                bool a = false;
                if (id != "")
                {
                    a = install(id);
                }
                if (a)
                {
                    MessageBox.Show("sine");
                }
            }*/
           
        }
        /// <summary>
        /// パッケージのインストール（複数あった場合選択してもらう）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool install(string id)
        {
            string args = "";
            int i = 0;
            var si = new ProcessStartInfo();
            {
                si.FileName = $@"{Environment.GetEnvironmentVariable("windir")}\System32\WindowsPowerShell\v1.0\powershell.exe";
                si.ArgumentList.Add("winget");
                si.ArgumentList.Add("install");
                si.ArgumentList.Add($"\"{id}\"");
                //si.ArgumentList.Add($"--");
                //si.Arguments = ($"winget search" +" "+ "\"\"" + name + "\"\"");
                si.CreateNoWindow = true;
                si.RedirectStandardOutput = true;
                si.RedirectStandardError = true;
                si.UseShellExecute = false;
                si.StandardOutputEncoding = Encoding.UTF8; // エンコーディング設定
            };

            using (var proc = new Process())
            {
                proc.EnableRaisingEvents = true;
                proc.StartInfo = si;
        
                proc.OutputDataReceived += (sender, ev) =>
                {
                    if (ev.Data is not null)
                    {
                        Debug.WriteLine($"{ev.Data}");
                        if (ev.Data.Contains(id))
                        {
                            args += ev.Data + "\n";
                            i++;
                        }

                    }
                    else
                        Debug.WriteLine($"標準出力 ev.Data がnull");
                };
                proc.ErrorDataReceived += (sender, ev) =>
                {
                    if (ev.Data is not null)
                        Debug.WriteLine($"標準エラー出力 {ev.Data}");
                    else
                        Debug.WriteLine($"標準エラー出力 ev.Data がnull");
                };
                proc.Exited += (sender, ev) =>
                {
                    Debug.WriteLine($"終了イベント到来");
                };

                // プロセス起動
                proc.Start();

                // 非同期出力読出し開始
                proc.BeginErrorReadLine();
                proc.BeginOutputReadLine();

                // 終了まで(同期的に)待つ
                proc.WaitForExit();
            }
            Debug.WriteLine("終了");

            return true;
        }
    }
}
