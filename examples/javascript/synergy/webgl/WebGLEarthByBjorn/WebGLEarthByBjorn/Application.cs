using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLEarthByBjorn;
using WebGLEarthByBjorn.Design;
using WebGLEarthByBjorn.HTML.Images.FromAssets;
using WebGLEarthByBjorn.HTML.Pages;
using ScriptCoreLib.Lambda;


namespace WebGLEarthByBjorn
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public IHTMLCanvas canvas;



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //http://thematicmapping.org/playground/webgl/earth/
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140222


            // Earth params
            var radius = 0.5;

            //var segments = 32;
            var segments = 128;
            var rotation = 6;

            var scene = new THREE.Scene();

            var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 0.01, 1000);
            camera.position.z = 1.5;

            var renderer = new THREE.WebGLRenderer(
                   new
                   {
                       preserveDrawingBuffer = true
                   }

                );
            renderer.setSize();

            scene.add(new THREE.AmbientLight(0x333333));

            var light = new THREE.DirectionalLight(0xffffff, 1);
            light.position.set(5, 3, 5);
            scene.add(light);



            #region sphere
            var sphere = new THREE.Mesh(
                new THREE.SphereGeometry(radius, segments, segments),
                new THREE.MeshPhongMaterial(
                        new
                        {
                            map = new THREE.Texture().With(
                                async s =>
                                {
                                    //0:75ms event: _2_no_clouds_4k_low view-source:36543
                                    //Application Cache Progress event (1 of 2) http://192.168.1.72:22248/view-source 192.168.1.72/:1
                                    //Application Cache Progress event (2 of 2)  192.168.1.72/:1
                                    //Application Cache Cached event 192.168.1.72/:1
                                    //1:1018ms event: _2_no_clouds_4k_low done view-source:36543
                                    //1:1019ms event: _2_no_clouds_4k view-source:36543
                                    //event.returnValue is deprecated. Please use the standard event.preventDefault() instead. view-source:2995
                                    //1:16445ms event: _2_no_clouds_4k done 

                                    // ~ tilde to open css editor?

                                    Console.WriteLine("event: _2_no_clouds_4k_low");
                                    s.image = await new _2_no_clouds_4k_low();
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: _2_no_clouds_4k_low done");

                                    await 20000;

                                    Console.WriteLine("event: _2_no_clouds_4k");
                                    s.image = await new _2_no_clouds_4k();
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: _2_no_clouds_4k done");
                                }
                            ),
                            bumpMap = THREE.ImageUtils.loadTexture(
                                new elev_bump_4k().src
                                //new elev_bump_4k_low().src
                                ),


                            // applies onyl to shaders to create the shadow
                            bumpScale = 0.005,

                            specularMap = new THREE.Texture().With(
                                async s =>
                                {
                                    Console.WriteLine("event: water_4k_low");
                                    s.image = await new water_4k_low();
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: water_4k_low done");

                                    await 20000;

                                    Console.WriteLine("event: water_4k");
                                    s.image = await new water_4k();
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: water_4k done");
                                }
                            ),


                            //specular =    new THREE.Color("grey")								
                            specular = new THREE.Color(0xa0a0a0)
                        })
            );
            #endregion

            // http://stackoverflow.com/questions/12447734/three-js-updateing-texture-on-plane


            sphere.rotation.y = rotation;
            scene.add(sphere);


            #region clouds
            var clouds = new THREE.Mesh(
                    new THREE.SphereGeometry(
                //radius + 0.003,
                        radius + 0.006,
                        segments, segments),
                    new THREE.MeshPhongMaterial(
                        new
                        {
                            //map = THREE.ImageUtils.loadTexture(
                            //    //new fair_clouds_4k().src
                            //    new fair_clouds_4k_low().src
                            //    ),


                            map = new THREE.Texture().With(
                                async s =>
                                {
                                    Console.WriteLine("event: fair_clouds_4k_low");
                                    s.image = await new fair_clouds_4k_low();
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: fair_clouds_4k_low done");

                                    await 20000;

                                    Console.WriteLine("event: fair_clouds_4k");
                                    s.image = await new fair_clouds_4k();
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: fair_clouds_4k done");
                                }
                            ),

                            transparent = true
                        })
                );
            clouds.rotation.y = rotation;
            scene.add(clouds);
            #endregion






            var stars = new THREE.Mesh(
                    new THREE.SphereGeometry(90, 64, 64),
                    new THREE.MeshBasicMaterial(
                    new
                    {
                        map = THREE.ImageUtils.loadTexture(new galaxy_starfield().src),
                        side = THREE.BackSide
                    })
                );
            scene.add(stars);

            //var controls = new THREE.TrackballControls(camera);
            //Native.document.body.style.margin = "0";
            //Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            this.canvas = (IHTMLCanvas)renderer.domElement;

            //renderer.domElement.AttachToDocument();
            this.canvas.AttachToDocument();
            this.canvas.style.SetLocation(0, 0);

            // jsc, what pointers do we have in store?
            this.canvas.css.style.cursor = IStyle.CursorEnum.pointer;
            this.canvas.css.active.style.cursor = IStyle.CursorEnum.move;


            var
                old = new
                {
                    sphere = new
                    {
                        sphere.rotation.x,
                        sphere.rotation.y

                    },
                    clouds = new
                    {
                        clouds.rotation.x,
                        clouds.rotation.y,
                    },


                    CursorX = 0,
                    CursorY = 0
                };

            #region onmousedown
            this.canvas.onmousedown +=
                e =>
                {
                    var pointerLock = this.canvas == Native.document.pointerLockElement;

                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        this.canvas.requestFullscreen();
                        this.canvas.requestPointerLock();
                    }
                    else
                    {
                        // movementX no longer works
                        old = new
                        {
                            sphere = new
                            {
                                sphere.rotation.x,
                                sphere.rotation.y

                            },
                            clouds = new
                            {
                                clouds.rotation.x,
                                clouds.rotation.y,
                            },


                            e.CursorX,
                            e.CursorY
                        };

                        if (pointerLock)
                        {
                            // skip
                        }
                        else
                        {
                            e.CaptureMouse();
                        }
                    }

                };
            #endregion





            var z = camera.position.z;

            var sfx = new WebGLEarthByBjorn.HTML.Audio.FromAssets.SatelliteBeep_Sputnik1
            {
                autobuffer = true,

                // this aint working
                //loop = true 

            };

            sfx.play();

            //sfx.AttachToHead();


            // http://soundfxnow.com/sound-fx/sputnik-satellite-beeping/

            this.canvas.onmousewheel +=
                e =>
                {
                    //camera.position.z = 1.5;

                    // min max. shall adjust speed also!
                    // max 4.0
                    // min 0.6
                    z -= 0.1 * e.WheelDirection;

                    z = z.Max(0.6).Min(4.5);

                    //Native.document.title = new { camera.position.z }.ToString();

                };

            // X:\jsc.svn\examples\javascript\Test\TestMouseMovement\TestMouseMovement\Application.cs
            #region onmousemove
            this.canvas.onmousemove +=
                e =>
                {
                    var pointerLock = this.canvas == Native.document.pointerLockElement;


                    //Console.WriteLine(new { e.MouseButton, pointerLock, e.movementX });

                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        if (pointerLock)
                        {
                            sphere.rotation.x += 0.01 * e.movementY;
                            sphere.rotation.y += 0.01 * e.movementX;

                            clouds.rotation.x += 0.01 * e.movementY;
                            clouds.rotation.y += 0.01 * e.movementX;
                        }
                        else
                        {
                            sphere.rotation.x = old.sphere.x + 0.01 * (e.CursorY - old.CursorY);
                            sphere.rotation.y = old.sphere.y + 0.01 * (e.CursorX - old.CursorX);

                            clouds.rotation.x = old.clouds.x + 0.01 * (e.CursorY - old.CursorY);
                            clouds.rotation.y = old.clouds.y + 0.01 * (e.CursorX - old.CursorX);
                        }


                        //    Native.document.title = new { e.movementX, e.movementY }.ToString();

                    }

                };
            #endregion


            // could we 
            Native.window.onframe +=
                e =>
                {
                    if (this.canvas.parentNode == null)
                        return;

                    camera.aspect = canvas.clientWidth / (double)canvas.clientHeight;
                    camera.updateProjectionMatrix();

                    camera.position.z += (z - camera.position.z) * e.delay.ElapsedMilliseconds / 200;


                    // the larger the vew the slower the rotation shall be
                    var speed = 0.0001 * e.delay.ElapsedMilliseconds +
                        0.007
                        * 96.0 / canvas.clientHeight
                        * 1.0 / camera.position.z;

                    //Native.document.title = new { s = 96.0 / canvas.clientHeight }.ToString();
                    //Native.document.title = new { speed }.ToString();


                    //controls.update();
                    sphere.rotation.y += speed;
                    clouds.rotation.y += speed;


                    renderer.render(scene, camera);
                };

            Native.window.onresize +=
                delegate
                {


                    //if (canvas.parentNode == Native.document.body)

                    // are we embedded?
                    if (page != null)
                        renderer.setSize();
                };


            //new IStyle(this.canvas.css.before)
            //{
            //    content = "'do a middle click to maximize the earth dashboard'",

            //    left = "1em",
            //    bottom = "1em",

            //    color = "white",

            //    position = IStyle.PositionEnum.absolute
            //};
        }

    }
}