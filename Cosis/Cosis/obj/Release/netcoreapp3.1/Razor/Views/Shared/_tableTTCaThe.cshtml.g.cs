#pragma checksum "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f388edb1bcdf577d853c76caa373b08e03e22e2a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__tableTTCaThe), @"mvc.1.0.view", @"/Views/Shared/_tableTTCaThe.cshtml")]
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
#line 1 "D:\ProjectC#\Cosis\Cosis\Views\_ViewImports.cshtml"
using Cosis;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ProjectC#\Cosis\Cosis\Views\_ViewImports.cshtml"
using Cosis.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\ProjectC#\Cosis\Cosis\Views\_ViewImports.cshtml"
using Cosis.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\ProjectC#\Cosis\Cosis\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f388edb1bcdf577d853c76caa373b08e03e22e2a", @"/Views/Shared/_tableTTCaThe.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"330bfff8c7c5f9dba2758406de113abfa6a13415", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__tableTTCaThe : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
  
    FormCosisContext context = new FormCosisContext();
    List<ThongTinCaThe> getTable()
    {
        string s = ViewBag.LoaiPhieu;
        return context.ThongTinCaThe.Where(x => x.MaLoaiPhieu == s).ToList();
    }
    int count(string macs, string thang, string nam)
    {
        return context.Master.Where(x => x.MaCoSo == macs && x.ThangDuTinh == thang && x.Nam == nam).Count();
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("          <tbody id=\"tBody\">\r\n");
#nullable restore
#line 14 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
     foreach (ThongTinCaThe m in @getTable())
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
         if (count(m.MaCoSo, ViewBag.thang, ViewBag.nam) > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr style=\"background-color: rgb(0, 160, 255); cursor:pointer;\"");
            BeginWriteAttribute("onclick", " onclick=\"", 663, "\"", 697, 3);
            WriteAttributeValue("", 673, "doiPhieu2_1(\'", 673, 13, true);
#nullable restore
#line 18 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
WriteAttributeValue("", 686, m.MaCoSo, 686, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 695, "\')", 695, 2, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <td class=\"text-center\">1</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 20 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.MaSoThue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 21 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.MaSoThue2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 22 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.MaCoSo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 23 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.TenCoSo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#nullable restore
#line 25 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr style=\"cursor:pointer;\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1213, "\"", 1247, 3);
            WriteAttributeValue("", 1223, "doiPhieu2_1(\'", 1223, 13, true);
#nullable restore
#line 28 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
WriteAttributeValue("", 1236, m.MaCoSo, 1236, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1245, "\')", 1245, 2, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <td class=\"text-center\">1</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 30 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.MaSoThue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 31 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.MaSoThue2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 32 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.MaCoSo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"text-center\">");
#nullable restore
#line 33 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
                                                       Write(m.TenCoSo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#nullable restore
#line 35 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\ProjectC#\Cosis\Cosis\Views\Shared\_tableTTCaThe.cshtml"
         

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("          </tbody>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591