using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLHand.HTML.Pages;

namespace WebGLHand
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.Shared.Drawing;
    using WebGLHand.Shaders;
    using WebGLHand.Library;
    using System.Collections.Generic;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=370 by Giles
         * 
         * 01. Created a new project of type Web Application
         * 02. initGL
         * 03. initShaders
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new __glMatrix().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           //new IFunction("alert(CanvasMatrix4);").apply(null);

                           InitializeContent(page);
                       };

                   source.AttachToDocument();
               }
           );


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page)
        {
            page.PageContainer.style.color = Color.Blue;

            var size = 500;

            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;
            #endregion

            #region gl - Initialise WebGL


            var gl = default(WebGLRenderingContext);

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                Native.Window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");
            }
            #endregion


            var gl_viewportWidth = size;
            var gl_viewportHeight = size;



            var shaderProgram = gl.createProgram();


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));

                    return null;
                }

                return shader;
            };
            #endregion

            var vs = createShader(new GeometryVertexShader());
            var fs = createShader(new GeometryFragmentShader());

            if (vs == null || fs == null) throw new InvalidOperationException("shader failed");

            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");

            gl.enableVertexAttribArray((ulong)shaderProgram_vertexPositionAttribute);

            // new in lesson 02
            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((ulong)shaderProgram_vertexColorAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");



            var mvMatrix = __glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = __glMatrix.mat4.create();

            #region new in lesson 03
            Action mvPushMatrix = delegate
            {
                var copy = __glMatrix.mat4.create();
                __glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };

            Action mvPopMatrix = delegate
            {
                mvMatrix = mvMatrixStack.Pop();
            };
            #endregion


            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);
                };
            #endregion




            #region init buffers


            #region cube
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            var cubesize = 1.0f * 0.10f;
            var vertices = new[]{
                // Front face
                -cubesize, -cubesize,  cubesize,
                 cubesize, -cubesize,  cubesize,
                 cubesize,  cubesize,  cubesize,
                -cubesize,  cubesize,  cubesize,

                // Back face
                -cubesize, -cubesize, -cubesize,
                -cubesize,  cubesize, -cubesize,
                 cubesize,  cubesize, -cubesize,
                 cubesize, -cubesize, -cubesize,

                // Top face
                -cubesize,  cubesize, -cubesize,
                -cubesize,  cubesize,  cubesize,
                 cubesize,  cubesize,  cubesize,
                 cubesize,  cubesize, -cubesize,

                // Bottom face
                -cubesize, -cubesize, -cubesize,
                 cubesize, -cubesize, -cubesize,
                 cubesize, -cubesize,  cubesize,
                -cubesize, -cubesize,  cubesize,

                // Right face
                 cubesize, -cubesize, -cubesize,
                 cubesize,  cubesize, -cubesize,
                 cubesize,  cubesize,  cubesize,
                 cubesize, -cubesize,  cubesize,

                // Left face
                -cubesize, -cubesize, -cubesize,
                -cubesize, -cubesize,  cubesize,
                -cubesize,  cubesize,  cubesize,
                -cubesize,  cubesize, -cubesize
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

            var cubeVertexPositionBuffer_itemSize = 3;
            var cubeVertexPositionBuffer_numItems = 36;

            var cubeVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);

            // 216, 191, 18
            var colors = new[]{
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face

                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face

                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face

                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face

                
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face

                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f  // Left face
            };



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var cubeVertexColorBuffer_itemSize = 4;
            var cubeVertexColorBuffer_numItems = 24;

            var cubeVertexIndexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
            var cubeVertexIndices = new UInt16[]{
                0, 1, 2,      0, 2, 3,    // Front face
                4, 5, 6,      4, 6, 7,    // Back face
                8, 9, 10,     8, 10, 11,  // Top face
                12, 13, 14,   12, 14, 15, // Bottom face
                16, 17, 18,   16, 18, 19, // Right face
                20, 21, 22,   20, 22, 23  // Left face
            };

            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
            var cubeVertexIndexBuffer_itemSize = 1;
            var cubeVertexIndexBuffer_numItems = 36;

            #endregion

            #endregion




            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.enable(gl.DEPTH_TEST);

            #region new in lesson 04

            var rPyramid = 0f;
            var rCube = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    rPyramid += (90 * elapsed) / 1000.0f;
                    rCube -= (75 * elapsed) / 1000.0f;
                }
                lastTime = timeNow;
            };

            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };
            #endregion

            // jsc error
            //var fdeg_state = new float[5];

            var fdeg_state = new int[5];
            // int array not initialized?

            fdeg_state[0] = 0;
            fdeg_state[1] = 66;
            fdeg_state[2] = 66;
            fdeg_state[3] = 0;
            fdeg_state[4] = 66;

            var fdeg_relax = new int[5];


            fdeg_relax[0] = 0;
            fdeg_relax[1] = 0;
            fdeg_relax[2] = 0;
            fdeg_relax[3] = 0;
            fdeg_relax[4] = 0;

            var fdeg_relaxstate = new int[5];


            fdeg_relaxstate[0] = 11;
            fdeg_relaxstate[1] = 11;
            fdeg_relaxstate[2] = 11;
            fdeg_relaxstate[3] = 11;
            fdeg_relaxstate[4] = 33;

            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


                __glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                __glMatrix.mat4.identity(mvMatrix);




                #region vertex
                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((ulong)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);
                #endregion

                #region color
                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);
                gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                #endregion


                __glMatrix.mat4.translate(mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });

                mvPushMatrix();


                // rotate all of it
                __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * 0.05f), new float[] { -1f, 0.5f, 0f });


                #region DrawCubeAt
                Action<float, float> DrawCubeAt =
                    (x, y) =>
                    {
                        mvPushMatrix();
                        __glMatrix.mat4.translate(mvMatrix, new float[] { 2 * cubesize * x, 2 * cubesize * -y, 0 });

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                        mvPopMatrix();
                    };
                #endregion

                for (int ix = 0; ix < 11; ix++)
                {
                    for (int iy = 0; iy < 11; iy++)
                    {
                        DrawCubeAt(ix, iy);

                    }
                }

                #region DrawFinger
                Action<int, float> DrawFinger =
                    (x, fdeg) =>
                    {
                        mvPushMatrix();


                        __glMatrix.mat4.rotate(mvMatrix, degToRad(fdeg), new float[] { 1f, 0f, 0f });

                        // 01.34.67.89.01
                        DrawCubeAt(3 * x + 0, -2);
                        DrawCubeAt(3 * x + 1, -2);
                        DrawCubeAt(3 * x + 0, -3);
                        DrawCubeAt(3 * x + 1, -3);

                        mvPushMatrix();

                        __glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (5), 0 });
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(fdeg), new float[] { 1f, 0f, 0f });
                        __glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (-5), 0 });

                        DrawCubeAt(3 * x + 0, -5);
                        DrawCubeAt(3 * x + 1, -5);
                        DrawCubeAt(3 * x + 0, -6);
                        DrawCubeAt(3 * x + 1, -6);

                        mvPushMatrix();

                        __glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (8), 0 });
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(fdeg), new float[] { 1f, 0f, 0f });
                        __glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (-8), 0 });

                        DrawCubeAt(3 * x + 0, -8);
                        DrawCubeAt(3 * x + 1, -8);
                        DrawCubeAt(3 * x + 0, -9);
                        DrawCubeAt(3 * x + 1, -9);


                        mvPopMatrix();
                        mvPopMatrix();
                        mvPopMatrix();
                    };
                #endregion





                // pinky
                DrawFinger(0, fdeg_state[0]);
                DrawFinger(1, fdeg_state[1]);
                // middle
                DrawFinger(2, fdeg_state[2]);
                // index
                DrawFinger(3, fdeg_state[3]);


                mvPushMatrix();

                __glMatrix.mat4.rotate(mvMatrix, degToRad(-90), new float[] { 0f, 0f, 1f });
                // we have misplaced it now. lets put it into its place:)
                __glMatrix.mat4.translate(mvMatrix, new float[] { 2 * cubesize * -4, 2 * cubesize * 11, 0 });




                // the thumb
                DrawFinger(4, fdeg_state[4]);

                mvPopMatrix();


                mvPopMatrix();


                #region original cube
                __glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 0.0f, 0.0f });

                mvPushMatrix();

                __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube), new float[] { 1f, 1f, 1f });

                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                setMatrixUniforms();
                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                mvPopMatrix();
                #endregion

            };
            drawScene();
            #endregion

            #region requestAnimFrame
            var requestAnimFrame = (IFunction)new IFunction(
                @"return window.requestAnimationFrame ||
         window.webkitRequestAnimationFrame ||
         window.mozRequestAnimationFrame ||
         window.oRequestAnimationFrame ||
         window.msRequestAnimationFrame ||
         function(/* function FrameRequestCallback */ callback, /* DOMElement Element */ element) {
           window.setTimeout(callback, 1000/60);
         };"
            ).apply(null);
            #endregion


            var c = 0;



            #region pinky
            page.f0.onmousedown +=
                delegate
                {
                    page.f0.style.color = Color.Blue;
                    fdeg_state[0] = 80;
                    fdeg_relax[0] = 0;
                };

            page.f0.onmouseup +=
                 delegate
                 {
                     page.f0.style.color = Color.None;
                     //fdeg_state[0] = 11;
                     fdeg_relax[0] = 1;
                 };
            #endregion

            #region finger
            page.f1.onmousedown +=
                delegate
                {
                    page.f0.style.color = Color.Blue;
                    fdeg_state[1] = 80;
                    fdeg_relax[1] = 0;
                };

            page.f1.onmouseup +=
                 delegate
                 {
                     page.f1.style.color = Color.None;
                     fdeg_relax[1] = 1;
                 };
            #endregion

            #region middle
            page.f2.onmousedown +=
                delegate
                {
                    page.f0.style.color = Color.Blue;
                    fdeg_state[2] = 80;
                    fdeg_relax[2] = 0;
                };

            page.f2.onmouseup +=
                 delegate
                 {
                     page.f1.style.color = Color.None;
                     fdeg_relax[2] = 1;
                 };
            #endregion

            #region index
            page.f3.onmousedown +=
                delegate
                {
                    page.f3.style.color = Color.Blue;
                    fdeg_state[3] = 80;
                    fdeg_relax[3] = 0;
                };

            page.f3.onmouseup +=
                 delegate
                 {
                     page.f3.style.color = Color.None;
                     fdeg_relax[3] = 1;
                 };
            #endregion

            #region thumb
            page.f4.onmousedown +=
                delegate
                {
                    page.f4.style.color = Color.Blue;
                    fdeg_state[4] = 80;
                    fdeg_relax[4] = 0;
                };

            page.f4.onmouseup +=
                 delegate
                 {
                     page.f4.style.color = Color.None;
                     fdeg_relax[4] = 1;
                 };
            #endregion


            #region rock
            page.fRock.onmousedown +=
                delegate
                {
                    page.fRock.style.color = Color.Blue;
                    fdeg_state[0] = 0;
                    fdeg_state[1] = 77;
                    fdeg_state[2] = 77;
                    fdeg_state[3] = 0;
                    fdeg_state[4] = 77;

                    fdeg_relax[0] = 0;
                    fdeg_relax[1] = 0;
                    fdeg_relax[2] = 0;
                    fdeg_relax[3] = 0;
                    fdeg_relax[4] = 0;
                };

            page.fRock.onmouseup +=
                 delegate
                 {
                     page.fRock.style.color = Color.None;

                     fdeg_relax[0] = 1;
                     fdeg_relax[1] = 1;
                     fdeg_relax[2] = 1;
                     fdeg_relax[3] = 1;
                     fdeg_relax[4] = 1;
                 };
            #endregion

            #region fFist
            page.fFist.onmousedown +=
                delegate
                {
                    page.fRock.style.color = Color.Blue;
                    fdeg_state[0] = 88;
                    fdeg_state[1] = 88;
                    fdeg_state[2] = 88;
                    fdeg_state[3] = 88;
                    fdeg_state[4] = 88;

                    fdeg_relax[0] = 0;
                    fdeg_relax[1] = 0;
                    fdeg_relax[2] = 0;
                    fdeg_relax[3] = 0;
                    fdeg_relax[4] = 0;
                };

            page.fFist.onmouseup +=
                 delegate
                 {
                     page.fFist.style.color = Color.None;

                     fdeg_relax[0] = 1;
                     fdeg_relax[1] = 1;
                     fdeg_relax[2] = 1;
                     fdeg_relax[3] = 1;
                     fdeg_relax[4] = 1;
                 };
            #endregion

            #region electric
            page.fElectric.onmousedown +=
                delegate
                {
                    page.fElectric.style.color = Color.Blue;
                    fdeg_state[0] = 0;
                    fdeg_state[1] = 0;
                    fdeg_state[2] = 0;
                    fdeg_state[3] = 0;
                    fdeg_state[4] = 0;

                    fdeg_relax[0] = 0;
                    fdeg_relax[1] = 0;
                    fdeg_relax[2] = 0;
                    fdeg_relax[3] = 0;
                    fdeg_relax[4] = 0;
                };

            page.fElectric.onmouseup +=
                 delegate
                 {
                     page.fElectric.style.color = Color.None;

                     fdeg_relax[0] = 1;
                     fdeg_relax[1] = 1;
                     fdeg_relax[2] = 1;
                     fdeg_relax[3] = 1;
                     fdeg_relax[4] = 1;
                 };
            #endregion

            page.fRelax.onclick +=
             delegate
             {
                 page.fElectric.style.color = Color.None;

                 fdeg_relax[0] = 1;
                 fdeg_relax[1] = 1;
                 fdeg_relax[2] = 1;
                 fdeg_relax[3] = 1;
                 fdeg_relax[4] = 1;
             };

            #region tick - new in lesson 03
            var tick = default(Action);

            tick = delegate
            {
                c++;

                for (int i = 0; i < 5; i++)
                {
                    if (fdeg_relax[i] > 0)
                    {
                        // Math.Sign(int) does not exist.
                        // next release should have it!

                        var a = (fdeg_state[i] - fdeg_relaxstate[i]);

                        if (a > 4)
                            fdeg_state[i] -= 3;

                        if (a < -4)
                            fdeg_state[i] += 3;

                    }
                }

                Native.Document.title = "" + c;

                drawScene();
                animate();

                requestAnimFrame.apply(null, IFunction.OfDelegate(tick));
            };

            tick();
            #endregion
        }

    }


}
