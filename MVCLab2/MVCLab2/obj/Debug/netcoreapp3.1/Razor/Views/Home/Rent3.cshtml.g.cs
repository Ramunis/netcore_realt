#pragma checksum "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36be9c277d599e6a208174878f8e750a17fcb31d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Rent3), @"mvc.1.0.view", @"/Views/Home/Rent3.cshtml")]
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
#nullable restore
#line 1 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\_ViewImports.cshtml"
using MVCLab2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\_ViewImports.cshtml"
using MVCLab2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36be9c277d599e6a208174878f8e750a17fcb31d", @"/Views/Home/Rent3.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"299329459b19d92bff71921d23cffe7bf0e8d26b", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Rent3 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MVCLab2.Models.rentboard>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Rented", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
  
    ViewData["Title"] = "Аренда";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    ");
#nullable restore
#line 7 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
Write(Html.ActionLink("По району", "Rent5"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 8 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
Write(Html.ActionLink("Самые дешёвые", "Rent1"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 9 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
Write(Html.ActionLink("Самые дорогие", "Rent2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 10 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
Write(Html.ActionLink("Min площадь", "Rent3"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 11 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
Write(Html.ActionLink("Max площадь", "Rent4"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <h1>Чтение из БД - Просмотр Договоров Аренды</h1>
    <table class=""table table-striped"">
        <thead>
            <tr>
                <th scope=""col"">ДЗ</th>
                <th scope=""col"">Клиент</th>
                <th scope=""col"">Реалтор</th>
                <th scope=""col"">Район</th>
                <th scope=""col"">Услуга</th>
                <th scope=""col"">Площадь</th>
                <th scope=""col"">ДС</th>
                <th scope=""col"">Срок</th>
                <th scope=""col"">Цена</th>
                <th scope=""col"">Оплата</th>
                <th scope=""col"">Ремонт</th>
                <th scope=""col""> </th>

            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 32 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 35 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.DZ);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 36 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Clientf);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 37 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Realtf);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 38 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.District);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 39 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Servic);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 40 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Sq);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 41 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.DS);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 42 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Term);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 43 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 44 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Pay);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 45 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                   Write(item.Repair);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36be9c277d599e6a208174878f8e750a17fcb31d8850", async() => {
                WriteLiteral("Купить");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 46 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
                                                                      WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 48 "C:\Users\sevam\source\repos\MVCLab2\MVCLab2\Views\Home\Rent3.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MVCLab2.Models.rentboard>> Html { get; private set; }
    }
}
#pragma warning restore 1591
