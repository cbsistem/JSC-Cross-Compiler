using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using THREELibrary.HTML.Pages;

namespace THREELibrary
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

//RewriteToAssembly error: System.ArgumentNullException: Value cannot be null.
//Parameter name: namedProperties[0]
//   at System.Reflection.Emit.CustomAttributeBuilder.InitCustomAttributeBuilder(ConstructorInfo con, Object[] constructorArgs, PropertyInfo[] namedProperties, Object[] propertyValues, FieldInfo[] namedFields, Object[] fieldValues)
//   at System.Reflection.Emit.CustomAttributeBuilder..ctor(ConstructorInfo con, Object[] constructorArgs, PropertyInfo[] namedProperties, Object[] propertyValues)
//   at jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins.IDLCompiler.<>c__DisplayClass52.<>c__DisplayClass60.<>c__DisplayClass69.<>c__DisplayClass6b.<Define>b__48(IDLParserToken param0)
//   at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 21
//   at jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins.IDLCompiler.<>c__DisplayClass52.<>c__DisplayClass60.<>c__DisplayClass69.<Define>b__45(IDLMemberAttribute SourceAttribute)

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
