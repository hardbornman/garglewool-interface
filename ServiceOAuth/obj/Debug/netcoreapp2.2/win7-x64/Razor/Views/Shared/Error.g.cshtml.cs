#pragma checksum "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ae9f91802d73c1032bfb1bec03e836a97006cb9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(AspNetCore.Views_Shared_Error))]
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
#line 1 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\_ViewImports.cshtml"
using ServiceOAuth;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ae9f91802d73c1032bfb1bec03e836a97006cb9", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1105b4588fa810cb6aaf5e432b43299b1ec51128", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ServiceOAuth.Quickstart.Home.ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(51, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
  
    var error = Model?.Error?.Error;
    var errorDescription = Model?.Error?.ErrorDescription;
    var request_id = Model?.Error?.RequestId;

#line default
#line hidden
            BeginContext(199, 232, true);
            WriteLiteral("\n<div class=\"error-page\">\n    <div class=\"page-header\">\n        <h1>Error</h1>\n    </div>\n\n    <div class=\"row\">\n        <div class=\"col-sm-6\">\n            <div class=\"alert alert-danger\">\n                Sorry, there was an error\n\n");
            EndContext();
#line 19 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
                 if (error != null)
                {

#line default
#line hidden
            BeginContext(485, 88, true);
            WriteLiteral("                    <strong>\n                        <em>\n                            : ");
            EndContext();
            BeginContext(574, 5, false);
#line 23 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
                         Write(error);

#line default
#line hidden
            EndContext();
            BeginContext(579, 61, true);
            WriteLiteral("\n                        </em>\n                    </strong>\n");
            EndContext();
#line 26 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"

                    if (errorDescription != null)
                    {

#line default
#line hidden
            BeginContext(713, 29, true);
            WriteLiteral("                        <div>");
            EndContext();
            BeginContext(743, 16, false);
#line 29 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
                        Write(errorDescription);

#line default
#line hidden
            EndContext();
            BeginContext(759, 7, true);
            WriteLiteral("</div>\n");
            EndContext();
#line 30 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
                    }
                }

#line default
#line hidden
            BeginContext(806, 20, true);
            WriteLiteral("            </div>\n\n");
            EndContext();
#line 34 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
             if (request_id != null)
            {

#line default
#line hidden
            BeginContext(877, 52, true);
            WriteLiteral("                <div class=\"request-id\">Request Id: ");
            EndContext();
            BeginContext(930, 10, false);
#line 36 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
                                               Write(request_id);

#line default
#line hidden
            EndContext();
            BeginContext(940, 7, true);
            WriteLiteral("</div>\n");
            EndContext();
#line 37 "D:\Code\06-硬骨头\GargleWool\ServiceOAuth\Views\Shared\Error.cshtml"
            }

#line default
#line hidden
            BeginContext(961, 33, true);
            WriteLiteral("        </div>\n    </div>\n</div>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ServiceOAuth.Quickstart.Home.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
