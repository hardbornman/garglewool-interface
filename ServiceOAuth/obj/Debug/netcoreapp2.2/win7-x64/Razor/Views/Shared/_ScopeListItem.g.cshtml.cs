#pragma checksum "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0a7a77ac1a2e35cf06d838ecb77bd85265829b4b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ScopeListItem), @"mvc.1.0.view", @"/Views/Shared/_ScopeListItem.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_ScopeListItem.cshtml", typeof(AspNetCore.Views_Shared__ScopeListItem))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\_ViewImports.cshtml"
using ServiceOAuth;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0a7a77ac1a2e35cf06d838ecb77bd85265829b4b", @"/Views/Shared/_ScopeListItem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1105b4588fa810cb6aaf5e432b43299b1ec51128", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ScopeListItem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ServiceOAuth.Controllers.Quickstart.Consent.ScopeViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(66, 152, true);
            WriteLiteral("\n<li class=\"list-group-item\">\n    <label>\n        <input class=\"consent-scopecheck\"\n               type=\"checkbox\"\n               name=\"ScopesConsented\"");
            EndContext();
            BeginWriteAttribute("id", "\n               id=\"", 218, "\"", 256, 2);
            WriteAttributeValue("", 238, "scopes_", 238, 7, true);
#line 8 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
WriteAttributeValue("", 245, Model.Name, 245, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("value", "\n               value=\"", 257, "\"", 291, 1);
#line 9 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
WriteAttributeValue("", 280, Model.Name, 280, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("checked", "\n               checked=\"", 292, "\"", 331, 1);
#line 10 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
WriteAttributeValue("", 317, Model.Checked, 317, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("disabled", "\n               disabled=\"", 332, "\"", 373, 1);
#line 11 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
WriteAttributeValue("", 358, Model.Required, 358, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(374, 4, true);
            WriteLiteral(" />\n");
            EndContext();
#line 12 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
         if (Model.Required)
        {

#line default
#line hidden
            BeginContext(417, 74, true);
            WriteLiteral("            <input type=\"hidden\"\n                   name=\"ScopesConsented\"");
            EndContext();
            BeginWriteAttribute("value", "\n                   value=\"", 491, "\"", 529, 1);
#line 16 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
WriteAttributeValue("", 518, Model.Name, 518, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(530, 4, true);
            WriteLiteral(" />\n");
            EndContext();
#line 17 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
        }

#line default
#line hidden
            BeginContext(544, 16, true);
            WriteLiteral("        <strong>");
            EndContext();
            BeginContext(561, 17, false);
#line 18 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
           Write(Model.DisplayName);

#line default
#line hidden
            EndContext();
            BeginContext(578, 10, true);
            WriteLiteral("</strong>\n");
            EndContext();
#line 19 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
         if (Model.Emphasize)
        {

#line default
#line hidden
            BeginContext(628, 71, true);
            WriteLiteral("            <span class=\"glyphicon glyphicon-exclamation-sign\"></span>\n");
            EndContext();
#line 22 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
        }

#line default
#line hidden
            BeginContext(709, 13, true);
            WriteLiteral("    </label>\n");
            EndContext();
#line 24 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
     if (Model.Required)
    {

#line default
#line hidden
            BeginContext(753, 41, true);
            WriteLiteral("        <span><em>(required)</em></span>\n");
            EndContext();
#line 27 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
    }

#line default
#line hidden
            BeginContext(800, 4, true);
            WriteLiteral("    ");
            EndContext();
#line 28 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
     if (Model.Description != null)
    {

#line default
#line hidden
            BeginContext(842, 60, true);
            WriteLiteral("        <div class=\"consent-description\">\n            <label");
            EndContext();
            BeginWriteAttribute("for", " for=\"", 902, "\"", 926, 2);
            WriteAttributeValue("", 908, "scopes_", 908, 7, true);
#line 31 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
WriteAttributeValue("", 915, Model.Name, 915, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(927, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(929, 17, false);
#line 31 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
                                       Write(Model.Description);

#line default
#line hidden
            EndContext();
            BeginContext(946, 24, true);
            WriteLiteral("</label>\n        </div>\n");
            EndContext();
#line 33 "E:\GitHub\garglewool-interface\ServiceOAuth\Views\Shared\_ScopeListItem.cshtml"
    }

#line default
#line hidden
            BeginContext(976, 5, true);
            WriteLiteral("</li>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ServiceOAuth.Controllers.Quickstart.Consent.ScopeViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
