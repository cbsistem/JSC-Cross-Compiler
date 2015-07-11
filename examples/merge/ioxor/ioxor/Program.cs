﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioxor
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // http://discutils.codeplex.com/SourceControl/latest#src/Vhd/Footer.cs

            // can we switch wifi channels?

            // need bit 1,2,3,4,5,7,8,9X histogram

            // https://ffmpeg.org/pipermail/ffmpeg-user/2011-August/002051.html
            // 60.1 KB (61,574 bytes)
            // 60.2 KB (61,652 bytes)
            // 78bytes

            // SIGINT
            // opencv

            // http://stackoverflow.com/questions/11986279/can-ffmpeg-convert-audio-from-raw-pcm-to-wav
            // header includes the format, sample rate, and number of channels.
            // http://www.topherlee.com/software/pcm-tut-wavformat.html






            // https://www.youtube.com/watch?v=2xZgCVG_Bzk
            // Audacity's "import Raw Data" feature.
            // Invalid PCM packet, data has size 2 but at least a size of 4 was expected
            // "R:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe" -f s16le -ar 44.1k -ac 2 -i file.pcm file.wav
            // ffmpeg -f u16le -ar 44100 -ac 1 -i input.raw
            // ffmpeg -f s16le -ar 44.1k -ac 2 -i file.pcm file.wav
            // http://stackoverflow.com/questions/11986279/can-ffmpeg-convert-audio-from-raw-pcm-to-wav

            // http://www.jsresources.org/examples/RawAudioDataConverter.html
            // http://www.mathworks.com/matlabcentral/answers/88840-how-to-open-a-headerless-wav-file

            // https://code.google.com/p/binvis/downloads/detail?name=BinVis_Binary_Release.zip&can=2&q=
            // https://code.google.com/p/cassia/
            // http://fragged.info/tag/remoteapp/

            File.WriteAllText("TerminalServerSession", "" + System.Windows.Forms.SystemInformation.TerminalServerSession);


            // send to RemoteApp
            Console.WriteLine(new { System.Windows.Forms.SystemInformation.TerminalServerSession });
            Console.WriteLine(new { Environment.CurrentDirectory });
            //Console.WriteLine(Environment.ter);

            var CrashManagerLastWriteback = File.Exists("LastWriteback") ? File.ReadAllText("LastWriteback") : "";

            Console.WriteLine(new { CrashManagerLastWriteback });

            var seed3 = Enumerable.Take(

                         from ff in Directory.GetFiles("x:/media/")
                         let fff = new FileInfo(ff)
                         // cutoff by date?
                         orderby fff.Length descending

                         select fff, 3
                     ).ToArray();

            foreach (var i in seed3)
            {
                Console.WriteLine(new { i.Name, i.Length, i.LastWriteTimeUtc });
            }

            foreach (var item in args)
            {
                var exists = Directory.Exists(item);

                Console.WriteLine(new { item, exists });

                if (exists)
                    foreach (var f in Directory.GetFiles(item))
                    {
                        if (File.Exists(f))
                        {
                            //Console.WriteLine(f);

                            var zf = new FileInfo(f);
                            var Length = zf.Length;

                            Console.WriteLine(new { f, Length, zf.LastWriteTimeUtc });




                            // { Length = 1 073 742 336 }
                            if (MessageBox.Show(

                                f, "update seed?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                var sw = Stopwatch.StartNew();

                                var seed3r = Enumerable.ToArray(
                                    from fff in seed3
                                    let fr = fff.OpenRead()
                                    // http://www.slideshare.net/DefCamp/defcamp-2013-doctrackr
                                    // SIGINT 55
                                    // offset by, ask UDP device

                                    let peek = fr.ReadByte()
                                    let peekdata = new byte[peek]
                                    let x = fr.Read(peekdata, 0, peek)

                                    select fr
                                );

                                Console.WriteLine(new { sw.ElapsedMilliseconds });


                                var r = File.Open(f, FileMode.Open, FileAccess.ReadWrite);


                                var c = 0;

                                Action yield = delegate { };

                                do
                                {
                                    var Position0 = r.Position;
                                    var data = new byte[0x1fffff];

                                    GC.KeepAlive(data);

                                    var c0 = r.Read(data, 0, data.Length);
                                    c = c0;

                                    // 255 read in 14
                                    // 65535 read in 15
                                    // 1048575 read in 104
                                    // 16777215 read in 1650

                                    Console.Title = (int)(100 * ((double)r.Position / (double)Length)) + "%";
                                    Console.WriteLine(r.Position + " read in " + sw.ElapsedMilliseconds);

                                    Task.Run(
                                        delegate
                                        {
                                            var sw0 = Stopwatch.StartNew();

                                            //Console.WriteLine("reading...");

                                            foreach (var seed0 in seed3r)
                                            {
                                                var data0 = new byte[data.Length];

                                                // apply entropy offset here?
                                                var c00 = seed0.Read(data0, 0, data0.Length);

                                                for (int j = 0; j < data.Length; j++)
                                                {
                                                    data[j] ^= data0[j];
                                                }

                                                //Console.WriteLine("reading... " + c0 + " in " + sw0.ElapsedMilliseconds);
                                                // 11534325 read in 1370
                                                //reading...
                                                //reading... 1048575 in 1
                                                //reading... 1048575 in 1
                                                //reading... 1048575 in 2
                                            }

                                            for (int j = 0; j < data.Length; j++)
                                            {
                                                data[j] ^= 0x55;
                                            }

                                            Console.WriteLine("reading... done in " + sw0.ElapsedMilliseconds);

                                            File.WriteAllText("ReadyForWriteback", "" + Position0);

                                            yield += delegate
                                            {
                                                r.Position = Position0;
                                                r.Write(data, 0, c0);
                                                r.Flush();

                                                //                                                0 written in 0
                                                //5242879 written in 0
                                                //10485758 written in 0
                                                //15728637 written in 0
                                                //18342170 written in 1

                                                //Console.WriteLine("critical writeback " + new { Position0 });

                                                Console.Title = (int)(100 * ((double)r.Position / (double)Length)) + "%";
                                                Console.WriteLine(r.Position + " written in " + sw.ElapsedMilliseconds);


                                                // if we crash we should fast forward to thisone?
                                                File.WriteAllText("LastWriteback", "" + Position0);
                                            };
                                        }
                                    );

                                    Thread.Yield();

                                }
                                while (c > 0);

                                // 1073742336 read in 414878
                                // 1 073 742 336 read in 414 878

                                //MessageBox.Show("do critical writeback?");

                                Thread.Yield();

                                for (int i = 10; i > 0; i--)
                                {
                                    Console.WriteLine("vsync. ready to ioxor " + i);
                                    Thread.Sleep(1000);
                                }

                                Thread.Sleep(300);

                                sw.Restart();
                                yield();

                            }

                        }
                    }


            }

            //new Form1().ShowDialog();
            // crash cleanup"
            MessageBox.Show("done. disconnect. keep in touch. reconnect.");
            // http://stackoverflow.com/questions/5207506/logoff-interactive-users-in-windows-from-a-service
            // http://microsoft.public.windows.terminal-services.narkive.com/14i4dvmL/programatically-reset-all-ts-sessions

        }
    }
}
