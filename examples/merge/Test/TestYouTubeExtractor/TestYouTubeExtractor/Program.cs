﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace TestYouTubeExtractor
{
    class Program
    {
        private static void DownloadVideo(IEnumerable<VideoInfo> videoInfos)
        {
            /*
             * Select the first .mp4 video with 360p resolution
             */
            VideoInfo video = videoInfos
                .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            /*
             * Create the video downloader.
             * The first argument is the video to download.
             * The second argument is the path to save the video file.
             */
            var videoDownloader = new VideoDownloader(video,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),

                video.Title.Replace(":", "_")
                        .Replace("*", "_") + video.VideoExtension));

            // Register the ProgressChanged event and print the current progress
            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            /*
             * Execute the video downloader.
             * For GUI applications note, that this method runs synchronously.
             */
            videoDownloader.Execute();
        }


        static void Main(string[] args)
        {
            // X:\jsc.svn\examples\merge\Test\TestJObjectParse\TestJObjectParse\Program.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150115/youtubeextractor

            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs
            // x:\jsc.svn\market\synergy\github\youtubeextractor\external\exampleapplication\program.cs

            // Our test youtube link
            const string link = "https://www.youtube.com/watch?v=SYM-RJwSGQ8";
            Debugger.Break();

            // rewrite broke JObject Parse.
            // Additional information: Bad JSON escape sequence: \5.Path 'args.afv_ad_tag_restricted_to_instream', line 1, position 3029.

            var extractedJson = "{\"assets\": {\"js\": \"\\/\\/s.ytimg.com\\/yts\\/jsbin\\/html5player-et_EE-vflOoSYPe\\/html5player.js\", \"css\": \"\\/\\/s.ytimg.com\\/yts\\/cssbin\\/www-player-vflM_Vw7-.css\", \"html\": \"\\/html5_player_template\"}, \"url_v9as2\": \"https:\\/\\/s.ytimg.com\\/yts\\/swfbin\\/player-vfl4nHN8A\\/cps.swf\", \"params\": {\"allowfullscreen\": \"true\", \"allowscriptaccess\": \"always\", \"bgcolor\": \"#000000\"}, \"url_v8\": \"https:\\/\\/s.ytimg.com\\/yts\\/swfbin\\/player-vfl4nHN8A\\/cps.swf\", \"messages\": {\"player_fallback\": [\"Video esitamiseks on vaja pistikprogrammi Adobe Flash Player v\\u00f5i HTML5 toega brauserit. \\u003ca href=\\\"http:\\/\\/get.adobe.com\\/flashplayer\\/\\\"\\u003eHankige uusim Flash Player\\u003c\\/a\\u003e \\u003ca href=\\\"\\/html5\\\"\\u003eLugege HTML5 toega brauseri installimise kohta\\u003c\\/a\\u003e\"]}, \"html5\": false, \"min_version\": \"8.0.0\", \"attrs\": {\"id\": \"movie_player\"}, \"url\": \"https:\\/\\/s.ytimg.com\\/yts\\/swfbin\\/player-vfl4nHN8A\\/watch_as3.swf\", \"args\": {\"iv_load_policy\": \"1\", \"of\": \"QbJiS1R-aUBpng2aB-1IdQ\", \"ucid\": \"UC0sVahwZgKFHjOBaxvm-a7Q\", \"loeid\": \"904843,930811,939985,950507\", \"vq\": \"auto\", \"vid\": \"SYM-RJwSGQ8\", \"as_launched_in_country\": \"1\", \"hl\": \"et_EE\", \"afv_ad_tag_restricted_to_instream\": \"http:\\/\\/googleads.g.doubleclick.net\\/pagead\\/ads?ad_type=skippablevideo\\u0026channel=yt_mpvid_Jx33JhIu2sujFIYv%2Byt_cid_10481%2Byt_no_ap%2Bytdevice_1%2Byt_no_cp%2Bafv_user_id_0sVahwZgKFHjOBaxvm-a7Q%2Bafv_user_tovelovevo%2Byt_inline_install%2Bytel_detailpage%2Bytps_default%2BVertical_36%2BVertical_211%2BVertical_592%2BVertical_613%2BVertical_1021%2BVertical_1030%2BVertical_1408%2Bafv_instream\\u0026client=ca-pub-6219811747049371\\u0026CPM=2500000\\u0026description_url=http%3A%2F%2Fwww.youtube.com%2Fvideo%2FSYM-RJwSGQ8\\u0026eid=27081898\\u0026hl=et\\u0026host=ca-host-pub-4404692103537709\\u0026ht_id=3816642\\u0026loeid=904843,930811,939985,950507\\u0026max_ad_duration=20000\\u0026url=http%3A%2F%2Fwww.youtube.com%2Fvideo%2FSYM-RJwSGQ8\\u0026video_cpm=2500000\\u0026ytdevice=1\\u0026yt_pt=APb3F29SJkXoqICjqfokeedeFjRBaWWNUWaAcl0Ngci9ppyWNSiJlQbBlbXku_yulEPy-bldY9IDYu_GMXsUTAFKPr55wqHBnvqha3fvRPM9FLTQnazZ94ghyv0RR-Pk2xNqT9a23UoW-Dw8nATj1hJkj3G8FHhNlA5mWpLwSg\", \"idpj\": \"-1\", \"pyv_in_related_cafe_experiment_id\": \"\", \"timestamp\": \"1421340039\", \"ptk\": \"vevo\", \"fexp\": \"900718,904843,927622,930811,932404,939985,941004,943917,947209,947218,947225,948124,950507,952302,952605,952901,955301,957103,957105,957201,959701\", \"iurlsd\": \"https:\\/\\/i.ytimg.com\\/vi\\/SYM-RJwSGQ8\\/sddefault.jpg\", \"iv_invideo_url\": \"https:\\/\\/www.youtube.com\\/annotations_invideo?cap_hist=1\\u0026cta=2\\u0026video_id=SYM-RJwSGQ8\", \"pyv_ad_channel\": \"yt_mpvid_Jx33JhIu2sujFIYv+yt_cid_10481+yt_no_ap+ytdevice_1+yt_no_cp+afv_user_id_0sVahwZgKFHjOBaxvm-a7Q+afv_user_tovelovevo+yt_inline_install\", \"view_count\": \"138837982\", \"dashmpd\": \"http:\\/\\/manifest.googlevideo.com\\/api\\/manifest\\/dash\\/playback_host\\/r9---sn-5go7dn7d.googlevideo.com\\/fexp\\/900718%2C904843%2C927622%2C930811%2C932404%2C939985%2C941004%2C943917%2C947209%2C947218%2C947225%2C948124%2C950507%2C952302%2C952605%2C952901%2C955301%2C957103%2C957105%2C957201%2C959701\\/upn\\/hb9WelerlWc\\/mm\\/31\\/source\\/youtube\\/ipbits\\/0\\/key\\/yt5\\/ip\\/83.191.187.255\\/sver\\/3\\/expire\\/1421361639\\/as\\/fmp4_audio_clear%2Cwebm_audio_clear%2Cfmp4_sd_hd_clear%2Cwebm_sd_hd_clear%2Cwebm2_sd_hd_clear\\/itag\\/0\\/s\\/89E89EC53DDF0907641F8AB9A3493B3BFB1412B01907.7548AB5598409F4DDDEB068ABDAB824EFE4D7C54\\/ms\\/au\\/id\\/o-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif\\/pl\\/19\\/mv\\/m\\/sparams\\/as%2Cid%2Cip%2Cipbits%2Citag%2Cmm%2Cms%2Cmv%2Cpl%2Cplayback_host%2Csource%2Cexpire\\/mt\\/1421339963\", \"iurlmaxres\": \"https:\\/\\/i.ytimg.com\\/vi\\/SYM-RJwSGQ8\\/maxresdefault.jpg\", \"ad_logging_flag\": \"1\", \"dash\": \"1\", \"uid\": \"0sVahwZgKFHjOBaxvm-a7Q\", \"aid\": \"P-vYpQpIGQ0\", \"midroll_prefetch_size\": \"1\", \"keywords\": \"Tove,Lo,Stay,High,Universal,Music,Pop\", \"allow_html5_ads\": \"1\", \"ad3_module\": \"1\", \"vm\": \"CAI\", \"aftv\": true, \"ad_tag\": \"https:\\/\\/ad.doubleclick.net\\/N4061\\/pfadx\\/com.ytpwatch.music\\/main_10481;kpeid=0sVahwZgKFHjOBaxvm-a7Q;kpid=10481;kpu=ToveLoVEVO;kvid=SYM-RJwSGQ8;mpvid=Jx33JhIu2sujFIYv;ssl=1;sz=WIDTHxHEIGHT;afv=1;dc_output=xml_vast3;dc_yt=1;dc_yt_pt=APb3F29SJkXoqICjqfokeedeFjRBaWWNUWaAcl0Ngci9ppyWNSiJlQbBlbXku_yulEPy-bldY9IDYu_GMXsUTAFKPr55wqHBnvqha3fvRPM9FLTQnazZ94ghyv0RR-Pk2xNqT9a23UoW-Dw8nATj1hJkj3G8FHhNlA5mWpLwSg;k2=3;k2=35;k2=36;k2=211;k2=592;k2=613;k2=1021;k2=1030;k2=1408;k5=3_35_36_211_592_613_1021_1030_1408;kclt=1;kga=-1;kgg=-1;klg=et;kmsrd=1;ko=p;kr=F;kvlg=en;kvz=205;nlfb=1;yt1st=1;yt3pav=1;ytcat=10;ytdevice=1;ytexp=904843,930811,939985,950507;yt_ec=2;yt_ec2=2;yt_vrallowed=1;!c=10481\", \"mpvid\": \"Jx33JhIu2sujFIYv\", \"atc\": \"a=3\\u0026b=XTEP-Xp4F-KRy5z0YDfiF76WC6U\\u0026c=1421340039\\u0026d=1\\u0026e=SYM-RJwSGQ8\\u0026c3a=19\\u0026c1a=1\\u0026hh=gRgfMZs4hDrMDrzVgtgGHs5MxA8\", \"ssl\": \"1\", \"csi_page_type\": \"watch,watch7ad\", \"ptchn\": \"0sVahwZgKFHjOBaxvm-a7Q\", \"mpu\": true, \"invideo\": true, \"allowed_ads\": \"[0, 1, 2, 4, 8, 9, 10]\", \"plid\": \"AAUMs4Qvl0XoyWds\", \"ad_slots\": \"0\", \"tag_for_child_directed\": false, \"iurl\": \"https:\\/\\/i.ytimg.com\\/vi\\/SYM-RJwSGQ8\\/hqdefault.jpg\", \"cid\": \"10481\", \"tmi\": \"1\", \"iv3_module\": \"1\", \"dynamic_allocation_ad_tag\": \"https:\\/\\/ad.doubleclick.net\\/N4061\\/pfadx\\/com.ytpwatch.music\\/main_10481;kpeid=0sVahwZgKFHjOBaxvm-a7Q;kpid=10481;kpu=ToveLoVEVO;kvid=SYM-RJwSGQ8;mpvid=Jx33JhIu2sujFIYv;ssl=1;sz=WIDTHxHEIGHT;afv=1;afvbase=eJxVkttu3CAQhp9muYojDj5xwUVTadtsFbVJpEjpDZq1J7FTbJAN9u7bBzuHOgg0_N_AcJiBWvuzQzX-a52Do8GprdGSqoG-R6POXncuIn04CXForgMfw8v--nHa8avoq6KH0bRkb7K3Gtw6rWOcCvV_Xi0cniYdRhx03EbHB2jmv8-_9j9ffl_BaeoSKG63i7yd0MQx2bcobW_aHqMZPRizMjS6Rg-tcfCMK3FjJE8QjI_yAQffVmC0yLeKM7aVmeRbmTOxlYxy9lUL-kWntHy_9XKzAaEjlR0GNODtoFjKmUgpFVJmBfn-50bxjC6NYFsrXtCSlbIkjVHoSWNHrypIFpu4cEzSlKa55PHMTBQFlaSJ31ArUbI8TzkxdgkiYwJScSFFjMWikbLMLmRGM1qQDk4aal2HAXxre8X5cnQfukhHxYjX7jwpPFUm1EjCYFTjvduJbzu-j32e58uzDT4c8bKyXSRreUR7_3iT3B3m-x-3JVlZzHD3-baPAlDsFbcQyFI;dc_backfill=1;dc_output=xml_vast3;dc_yt=1;dc_yt_pt=APb3F29SJkXoqICjqfokeedeFjRBaWWNUWaAcl0Ngci9ppyWNSiJlQbBlbXku_yulEPy-bldY9IDYu_GMXsUTAFKPr55wqHBnvqha3fvRPM9FLTQnazZ94ghyv0RR-Pk2xNqT9a23UoW-Dw8nATj1hJkj3G8FHhNlA5mWpLwSg;k2=3;k2=35;k2=36;k2=211;k2=592;k2=613;k2=1021;k2=1030;k2=1408;k5=3_35_36_211_592_613_1021_1030_1408;kclt=1;kga=-1;kgg=-1;klg=et;kmsrd=1;ko=p;kr=F;kvlg=en;kvz=205;nlfb=1;yt1st=1;yt3pav=1;ytcat=10;ytdevice=1;ytexp=904843,930811,939985,950507;yt_ec=2;yt_ec2=2;yt_vrallowed=1;!c=10481\", \"t\": \"1\", \"thumbnail_url\": \"https:\\/\\/i.ytimg.com\\/vi\\/SYM-RJwSGQ8\\/default.jpg\", \"shortform\": true, \"loudness\": \"-18.6090011597\", \"instream_long\": false, \"iurlmq\": \"https:\\/\\/i.ytimg.com\\/vi\\/SYM-RJwSGQ8\\/mqdefault.jpg\", \"probe_url\": \"http:\\/\\/r9---sn-nwj7km7r.googlevideo.com\\/videogoodput?id=o-AIZFb8azlRSD64KtLX8CylqkmOqMvVcRH-5qOdiAjKPG\\u0026source=goodput\\u0026range=0-99999\\u0026expire=1421343639\\u0026ip=83.191.187.255\\u0026ms=pm\\u0026mm=35\\u0026nh=EAM\\u0026sparams=id,source,range,expire,ip,ms,mm,nh\\u0026signature=385F09B43D02F7A481A44CE0E6B060803A7C3629.5B6ED02A6C2B5B41C2C269E7A820B958EA7A6935\\u0026key=cms1\", \"fmt_list\": \"22\\/1280x720\\/9\\/0\\/115,43\\/640x360\\/99\\/0\\/0,18\\/640x360\\/9\\/0\\/115,5\\/426x240\\/7\\/0\\/0,36\\/426x240\\/99\\/1\\/0,17\\/256x144\\/99\\/1\\/0\", \"afv_invideo_ad_tag\": \"http:\\/\\/googleads.g.doubleclick.net\\/pagead\\/ads?ad_type=text_image_flash\\u0026channel=yt_mpvid_Jx33JhIu2sujFIYv%2Byt_cid_10481%2Byt_no_ap%2Bytdevice_1%2Byt_no_cp%2Bafv_user_id_0sVahwZgKFHjOBaxvm-a7Q%2Bafv_user_tovelovevo%2Byt_inline_install%2Bytel_detailpage%2Bytps_default%2BVertical_36%2BVertical_211%2BVertical_592%2BVertical_613%2BVertical_1021%2BVertical_1030%2BVertical_1408%2Bafv_overlay%2Binvideo_overlay_480x70_cat10\\u0026client=ca-pub-6219811747049371\\u0026description_url=http%3A%2F%2Fwww.youtube.com%2Fvideo%2FSYM-RJwSGQ8\\u0026eid=27081898\\u0026hl=et\\u0026host=ca-host-pub-4404692103537709\\u0026ht_id=3816642\\u0026loeid=904843,930811,939985,950507\\u0026url=http%3A%2F%2Fwww.youtube.com%2Fvideo%2FSYM-RJwSGQ8\\u0026ytdevice=1\\u0026yt_pt=APb3F29SJkXoqICjqfokeedeFjRBaWWNUWaAcl0Ngci9ppyWNSiJlQbBlbXku_yulEPy-bldY9IDYu_GMXsUTAFKPr55wqHBnvqha3fvRPM9FLTQnazZ94ghyv0RR-Pk2xNqT9a23UoW-Dw8nATj1hJkj3G8FHhNlA5mWpLwSg\", \"adsense_video_doc_id\": \"yt_SYM-RJwSGQ8\", \"allow_ratings\": \"1\", \"video_id\": \"SYM-RJwSGQ8\", \"focEnabled\": \"1\", \"enablejsapi\": 1, \"gut_tag\": \"\\/4061\\/ytpwatch\\/main_10481\", \"enablecsi\": \"1\", \"remarketing_url\": \"https:\\/\\/googleads.g.doubleclick.net\\/pagead\\/viewthroughconversion\\/962985656\\/?cver=20150106\\u0026foc_id=0sVahwZgKFHjOBaxvm-a7Q\\u0026aid=P-vYpQpIGQ0\\u0026label=followon_view\\u0026ptype=view\\u0026data=backend%3Dinnertube%3Bcname%3D1%3Bcver%3D20150106%3Bptype%3Dview%3Btype%3Dview%3Butuid%3D0sVahwZgKFHjOBaxvm-a7Q%3Butvid%3DSYM-RJwSGQ8\\u0026backend=innertube\\u0026cname=1\", \"no_get_video_log\": \"1\", \"watermark\": \",https:\\/\\/s.ytimg.com\\/yts\\/img\\/watermark\\/youtube_watermark-vflHX6b6E.png,https:\\/\\/s.ytimg.com\\/yts\\/img\\/watermark\\/youtube_hd_watermark-vflAzLcD6.png\", \"token\": \"1\", \"avg_rating\": \"4.87827730179\", \"rmktEnabled\": \"1\", \"excluded_ads\": \"2=1_2,2_2\", \"ad_preroll\": \"1\", \"length_seconds\": \"264\", \"eventid\": \"h-23VMu9NImmwAOD7IDgBA\", \"ad_device\": \"1\", \"host_language\": \"et\", \"adaptive_fmts\": \"init=0-712\\u0026size=1920x1080\\u0026index=713-1332\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fmp4%26expire%3D1421361639%26itag%3D137%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.961%26mm%3D31%26gir%3Dyes%26clen%3D62952406%26lmt%3D1399591612697869%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=137\\u0026clen=62952406\\u0026bitrate=3683598\\u0026s=66E66E4D4E76E0E550EF87E6D69DBB84190E5B3C15E4.54BF33EBD76E35EB210ABD9B24F853FB231B9DB8\\u0026lmt=1399591612697869\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.640028%22\\u0026projection_type=1\\u0026fps=25,init=0-234\\u0026size=1920x1080\\u0026index=235-1087\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fwebm%26expire%3D1421361639%26itag%3D248%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.960%26mm%3D31%26gir%3Dyes%26clen%3D41121823%26lmt%3D1396710884098738%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=248\\u0026clen=41121823\\u0026bitrate=2007217\\u0026s=FA0FA06E4578D0921F4A03DA12A26064AA6220E4F5DC.C67EECA1CBBBAF0CC25BE76AF0855AA9D1DD0627\\u0026lmt=1396710884098738\\u0026type=video%2Fwebm%3B+codecs%3D%22vp9%22\\u0026projection_type=1\\u0026fps=1,init=0-710\\u0026size=1280x720\\u0026index=711-1330\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fmp4%26expire%3D1421361639%26itag%3D136%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.961%26mm%3D31%26gir%3Dyes%26clen%3D32441129%26lmt%3D1399591567577595%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=136\\u0026clen=32441129\\u0026bitrate=2204967\\u0026s=BD6BD63B7BBAC0CEB3A00E9D6D8F29D2B7CA0DB0139B.24C9CF7AF627AE8655E8BB6D2371AA1F38C3FB2C\\u0026lmt=1399591567577595\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.4d401f%22\\u0026projection_type=1\\u0026fps=25,init=0-234\\u0026size=1280x720\\u0026index=235-1075\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fwebm%26expire%3D1421361639%26itag%3D247%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.960%26mm%3D31%26gir%3Dyes%26clen%3D24587522%26lmt%3D1396710826947076%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=247\\u0026clen=24587522\\u0026bitrate=1284253\\u0026s=337337D38ED241833F47C703430037D64D1008F671B1.435493C968CA83B25736AA84F15EE7BF01D91126\\u0026lmt=1396710826947076\\u0026type=video%2Fwebm%3B+codecs%3D%22vp9%22\\u0026projection_type=1\\u0026fps=1,init=0-709\\u0026size=854x480\\u0026index=710-1329\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fmp4%26expire%3D1421361639%26itag%3D135%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.961%26mm%3D31%26gir%3Dyes%26clen%3D16776132%26lmt%3D1399591558998485%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=135\\u0026clen=16776132\\u0026bitrate=1103124\\u0026s=F87F8757CCBBB2FF9B10F7881140D189351A5494219A.CBB8B367DA4E23AADEE0DA4464E0EF63A55B5EF4\\u0026lmt=1399591558998485\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.4d401e%22\\u0026projection_type=1\\u0026fps=25,init=0-234\\u0026size=854x480\\u0026index=235-1058\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fwebm%26expire%3D1421361639%26itag%3D244%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.960%26mm%3D31%26gir%3Dyes%26clen%3D13833317%26lmt%3D1396710854749711%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=244\\u0026clen=13833317\\u0026bitrate=650334\\u0026s=153153E7FE16DE8A934DF175AB0BA0401F608AF0F178.10923E9F906531A13231EF43C03A49C11F5F2B88\\u0026lmt=1396710854749711\\u0026type=video%2Fwebm%3B+codecs%3D%22vp9%22\\u0026projection_type=1\\u0026fps=1,init=0-709\\u0026size=640x360\\u0026index=710-1329\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fmp4%26expire%3D1421361639%26itag%3D134%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.961%26mm%3D31%26gir%3Dyes%26clen%3D8441677%26lmt%3D1399591557619827%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=134\\u0026clen=8441677\\u0026bitrate=602706\\u0026s=2722728B218552FD0602C907AEF76389D6A24CC09CC9.86AD114A8D4457EB0158F90165255A06A5D969EE\\u0026lmt=1399591557619827\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.4d401e%22\\u0026projection_type=1\\u0026fps=25,init=0-234\\u0026size=640x360\\u0026index=235-1058\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fwebm%26expire%3D1421361639%26itag%3D243%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.960%26mm%3D31%26gir%3Dyes%26clen%3D8176355%26lmt%3D1396710819942710%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=243\\u0026clen=8176355\\u0026bitrate=349371\\u0026s=A6AA6A5C3FE9033513901286B51C91A8D7C84346B72A.220BAF71C289E1D207D873FA1148C3BCC4FF6615\\u0026lmt=1396710819942710\\u0026type=video%2Fwebm%3B+codecs%3D%22vp9%22\\u0026projection_type=1\\u0026fps=1,init=0-674\\u0026size=426x240\\u0026index=675-1294\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fmp4%26expire%3D1421361639%26itag%3D133%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.961%26mm%3D31%26gir%3Dyes%26clen%3D7947750%26lmt%3D1399591552096674%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=133\\u0026clen=7947750\\u0026bitrate=274807\\u0026s=BFCBFCE97EC7145C232F8A0FE81B17E1E36437AC816E.59D6866E85876259688D87A2BA5B3F2870FE8EA0\\u0026lmt=1399591552096674\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.4d4015%22\\u0026projection_type=1\\u0026fps=25,init=0-233\\u0026size=426x240\\u0026index=234-1056\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fwebm%26expire%3D1421361639%26itag%3D242%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.960%26mm%3D31%26gir%3Dyes%26clen%3D4463385%26lmt%3D1396710812463469%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=242\\u0026clen=4463385\\u0026bitrate=176525\\u0026s=45445463C4023D4F9F9625F5B43E9402F0AABC12C850.5EE51A0EABDE5A19EC41D7FF6C3712FD32833910\\u0026lmt=1396710812463469\\u0026type=video%2Fwebm%3B+codecs%3D%22vp9%22\\u0026projection_type=1\\u0026fps=1,init=0-671\\u0026size=256x144\\u0026index=672-1291\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Dvideo%252Fmp4%26expire%3D1421361639%26itag%3D160%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.961%26mm%3D31%26gir%3Dyes%26clen%3D3566826%26lmt%3D1399591550864090%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=160\\u0026clen=3566826\\u0026bitrate=112431\\u0026s=B2AB2A8448C63B5847CF8DC2BB527D30607413562AF5.86BA5D00E17923E0121B741BF54F5F942D5BF546\\u0026lmt=1399591550864090\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.4d400c%22\\u0026projection_type=1\\u0026fps=13,bitrate=129503\\u0026s=5295298F2851FF905729357244465E485EC6F02F0B0B.B9D1C2CB25EEB8050326F73F0AF3B008D3C811CB\\u0026clen=4223810\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Daudio%252Fmp4%26expire%3D1421361639%26itag%3D140%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D263.058%26mm%3D31%26gir%3Dyes%26clen%3D4223810%26lmt%3D1399591547991012%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=140\\u0026type=audio%2Fmp4%3B+codecs%3D%22mp4a.40.2%22\\u0026init=0-591\\u0026lmt=1399591547991012\\u0026index=592-947\\u0026projection_type=1,init=0-4451\\u0026index=4452-4907\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fsource%3Dyoutube%26keepalive%3Dyes%26mime%3Daudio%252Fwebm%26expire%3D1421361639%26itag%3D171%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26sparams%3Dclen%252Cdur%252Cgir%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Ckeepalive%252Clmt%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26dur%3D262.994%26mm%3D31%26gir%3Dyes%26clen%3D3937230%26lmt%3D1396710796847202%26ms%3Dau%26mv%3Dm%26mt%3D1421339963%26ipbits%3D0%26key%3Dyt5%26ip%3D83.191.187.255%26upn%3Dhb9WelerlWc%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26initcwndbps%3D1730000%26sver%3D3%26pl%3D19\\u0026itag=171\\u0026clen=3937230\\u0026bitrate=135444\\u0026s=725725A58EFD28E67CD4D422600BA746F610FD474E08.2EA437F310D6A9BE5B5CA0382E695BA55FC61220\\u0026lmt=1396710796847202\\u0026type=audio%2Fwebm%3B+codecs%3D%22vorbis%22\\u0026projection_type=1\\u0026fps=1\", \"storyboard_spec\": \"https:\\/\\/i.ytimg.com\\/sb\\/SYM-RJwSGQ8\\/storyboard3_L$L\\/$N.jpg|48#27#100#10#10#0#default#taDBPdvONVcRJ1VZHTye6OXXRE0|80#45#133#10#10#2000#M$M#QVcKKrIE_56cae8-zunl0Z6J-OI|160#90#133#5#5#2000#M$M#PlbtzxMPEZnYLpjf6eqY802H7GY\", \"cafe_experiment_id\": \"27081898\", \"show_content_thumbnail\": true, \"iv_module\": \"https:\\/\\/s.ytimg.com\\/yts\\/swfbin\\/player-vfl4nHN8A\\/iv_module.swf\", \"afv\": true, \"dclk\": true, \"baseUrl\": \"https:\\/\\/googleads.g.doubleclick.net\\/pagead\\/viewthroughconversion\\/962985656\\/\", \"max_dynamic_allocation_ad_tag_length\": \"2040\", \"cl\": \"83543002\", \"ytfocEnabled\": \"1\", \"advideo\": \"1\", \"afv_ad_tag\": \"http:\\/\\/googleads.g.doubleclick.net\\/pagead\\/ads?ad_type=skippablevideo\\u0026channel=yt_mpvid_Jx33JhIu2sujFIYv%2Byt_cid_10481%2Byt_no_ap%2Bytdevice_1%2Byt_no_cp%2Bafv_user_id_0sVahwZgKFHjOBaxvm-a7Q%2Bafv_user_tovelovevo%2Byt_inline_install%2Bytel_detailpage%2Bytps_default%2BVertical_36%2BVertical_211%2BVertical_592%2BVertical_613%2BVertical_1021%2BVertical_1030%2BVertical_1408%2Bafv_instream\\u0026client=ca-pub-6219811747049371\\u0026CPM=2500000\\u0026description_url=http%3A%2F%2Fwww.youtube.com%2Fvideo%2FSYM-RJwSGQ8\\u0026eid=27081898\\u0026hl=et\\u0026host=ca-host-pub-4404692103537709\\u0026ht_id=3816642\\u0026loeid=904843,930811,939985,950507\\u0026max_ad_duration=20000\\u0026url=http%3A%2F%2Fwww.youtube.com%2Fvideo%2FSYM-RJwSGQ8\\u0026video_cpm=2500000\\u0026ytdevice=1\\u0026yt_pt=APb3F29SJkXoqICjqfokeedeFjRBaWWNUWaAcl0Ngci9ppyWNSiJlQbBlbXku_yulEPy-bldY9IDYu_GMXsUTAFKPr55wqHBnvqha3fvRPM9FLTQnazZ94ghyv0RR-Pk2xNqT9a23UoW-Dw8nATj1hJkj3G8FHhNlA5mWpLwSg\", \"oid\": \"yZJtHRi2SvzOOKZfbA-GRA\", \"show_pyv_in_related\": true, \"trueview\": true, \"sffb\": true, \"cr\": \"EE\", \"allow_embed\": \"1\", \"loaderUrl\": \"https:\\/\\/www.youtube.com\\/watch?v=SYM-RJwSGQ8\", \"c\": \"WEB\", \"ldpj\": \"-20\", \"videostats_playback_base_url\": \"https:\\/\\/s.youtube.com\\/api\\/stats\\/playback?plid=AAUMs4Qvl0XoyWds\\u0026of=QbJiS1R-aUBpng2aB-1IdQ\\u0026cl=83543002\\u0026len=264\\u0026vm=CAI\\u0026fexp=900718%2C904843%2C927622%2C930811%2C932404%2C939985%2C941004%2C943917%2C947209%2C947218%2C947225%2C948124%2C950507%2C952302%2C952605%2C952901%2C955301%2C957103%2C957105%2C957201%2C959701\\u0026docid=SYM-RJwSGQ8\\u0026ei=h-23VMu9NImmwAOD7IDgBA\\u0026ns=yt\", \"title\": \"Tove Lo - Habits (Stay High) - Hippie Sabotage Remix\", \"ad_flags\": \"0\", \"pltype\": \"content\", \"iurlhq\": \"https:\\/\\/i.ytimg.com\\/vi\\/SYM-RJwSGQ8\\/hqdefault.jpg\", \"midroll_freqcap\": \"420.0\", \"url_encoded_fmt_stream_map\": \"fallback_host=tc.v4.cache2.googlevideo.com\\u0026quality=hd720\\u0026s=31431459B9FBC8BE56302B911C21CE94E0C0002A431E.98A307348C6441C8C7CD73D5D227FD3F9406DC79\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fdur%3D263.058%26key%3Dyt5%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26mm%3D31%26source%3Dyoutube%26ratebypass%3Dyes%26ipbits%3D0%26initcwndbps%3D1730000%26mime%3Dvideo%252Fmp4%26ip%3D83.191.187.255%26sver%3D3%26upn%3Dhb9WelerlWc%26expire%3D1421361639%26itag%3D22%26ms%3Dau%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26pl%3D19%26mv%3Dm%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Cratebypass%252Csource%252Cupn%252Cexpire%26mt%3D1421339963\\u0026itag=22\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.64001F%2C+mp4a.40.2%22,fallback_host=tc.v16.cache2.googlevideo.com\\u0026quality=medium\\u0026s=FEFFEF33A394D313735E984E12C03745DABB40301ADC.A6265ED23893AFFFFC5D8C788985D5069164E6FC\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fdur%3D0.000%26key%3Dyt5%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26mm%3D31%26source%3Dyoutube%26ratebypass%3Dyes%26ipbits%3D0%26initcwndbps%3D1730000%26mime%3Dvideo%252Fwebm%26ip%3D83.191.187.255%26sver%3D3%26upn%3Dhb9WelerlWc%26expire%3D1421361639%26itag%3D43%26ms%3Dau%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26pl%3D19%26mv%3Dm%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Cratebypass%252Csource%252Cupn%252Cexpire%26mt%3D1421339963\\u0026itag=43\\u0026type=video%2Fwebm%3B+codecs%3D%22vp8.0%2C+vorbis%22,fallback_host=tc.v23.cache5.googlevideo.com\\u0026quality=medium\\u0026s=DE3DE3ED38C4981EF675E26E62337B1F41BD9F7FB3FD.C032F24E6C7894CC689ACF4495EC840DCF38CD91\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fdur%3D263.058%26key%3Dyt5%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26mm%3D31%26source%3Dyoutube%26ratebypass%3Dyes%26ipbits%3D0%26initcwndbps%3D1730000%26mime%3Dvideo%252Fmp4%26ip%3D83.191.187.255%26sver%3D3%26upn%3Dhb9WelerlWc%26expire%3D1421361639%26itag%3D18%26ms%3Dau%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26pl%3D19%26mv%3Dm%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Cratebypass%252Csource%252Cupn%252Cexpire%26mt%3D1421339963\\u0026itag=18\\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.42001E%2C+mp4a.40.2%22,fallback_host=tc.v4.cache6.googlevideo.com\\u0026quality=small\\u0026s=ACFACF688D3AC96A7F5B659C9733CA2C48116A28883A.D7CEEAF99183D0A05C815BD5B8DF05260C5B8156\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fdur%3D263.053%26key%3Dyt5%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26mm%3D31%26source%3Dyoutube%26ipbits%3D0%26initcwndbps%3D1730000%26mime%3Dvideo%252Fx-flv%26ip%3D83.191.187.255%26sver%3D3%26upn%3Dhb9WelerlWc%26expire%3D1421361639%26itag%3D5%26ms%3Dau%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26pl%3D19%26mv%3Dm%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26mt%3D1421339963\\u0026itag=5\\u0026type=video%2Fx-flv,fallback_host=tc.v23.cache3.googlevideo.com\\u0026quality=small\\u0026s=38F38FBEBAF0AB9F12E027481AA75E218B77CFE3A65D.4841FDBCC5B17C9C0827F7F98CBE3B255DE8C75D\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fdur%3D263.174%26key%3Dyt5%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26mm%3D31%26source%3Dyoutube%26ipbits%3D0%26initcwndbps%3D1730000%26mime%3Dvideo%252F3gpp%26ip%3D83.191.187.255%26sver%3D3%26upn%3Dhb9WelerlWc%26expire%3D1421361639%26itag%3D36%26ms%3Dau%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26pl%3D19%26mv%3Dm%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26mt%3D1421339963\\u0026itag=36\\u0026type=video%2F3gpp%3B+codecs%3D%22mp4v.20.3%2C+mp4a.40.2%22,fallback_host=tc.v6.cache8.googlevideo.com\\u0026quality=small\\u0026s=91F91FD93250709CA1863D81EF24577153F05DAF165F.31AC6A17996A512B1B8D906577BCA0D6B65CF305\\u0026url=http%3A%2F%2Fr9---sn-5go7dn7d.googlevideo.com%2Fvideoplayback%3Fdur%3D263.174%26key%3Dyt5%26fexp%3D900718%252C904843%252C927622%252C930811%252C932404%252C939985%252C941004%252C943917%252C947209%252C947218%252C947225%252C948124%252C950507%252C952302%252C952605%252C952901%252C955301%252C957103%252C957105%252C957201%252C959701%26mm%3D31%26source%3Dyoutube%26ipbits%3D0%26initcwndbps%3D1730000%26mime%3Dvideo%252F3gpp%26ip%3D83.191.187.255%26sver%3D3%26upn%3Dhb9WelerlWc%26expire%3D1421361639%26itag%3D17%26ms%3Dau%26id%3Do-AGH1AfmnZMbNhZ90hEk2ToehjVwUh5NIojI6kwDUTaif%26pl%3D19%26mv%3Dm%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cpl%252Csource%252Cupn%252Cexpire%26mt%3D1421339963\\u0026itag=17\\u0026type=video%2F3gpp%3B+codecs%3D%22mp4v.20.3%2C+mp4a.40.2%22\", \"author\": \"ToveLoVEVO\", \"watch_xlb\": \"https:\\/\\/s.ytimg.com\\/yts\\/xlbbin\\/watch-strings-et_EE-vflDoPw-9.xlb\", \"afv_instream_max\": \"20000\", \"account_playback_token\": \"QUFFLUhqa016Q0JPdmJsbGdqdFpIcnhyN0VrbXE3YTlUd3xBQ3Jtc0tucnhtS0V4VlR1SUp3cEpUdk1qYXE3ZVN2cXlNVzVDX1JUNk1YTnhyTlFYVDJlMk9BVUM5akFMNGZVcFNiZ243X2VkMWlwWktrbWEtWjFwVjNCeFJSUmpOYmEyalVxd0gzQU1LYXFRYS1hMTNXZGVfOA==\"}, \"sts\": 16421}";
            var j = JObject.Parse(extractedJson);

            // jsc rewriter breaks it?
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
            // Additional information: The remote name could not be resolved: 'youtube.com'

            //DownloadAudio(videoInfos);
            DownloadVideo(videoInfos);
        }
    }
}
