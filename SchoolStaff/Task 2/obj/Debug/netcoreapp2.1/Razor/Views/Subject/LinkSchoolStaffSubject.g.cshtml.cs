#pragma checksum "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ee4cbd031f8e55539e48b1c14471930752f69fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Subject_LinkSchoolStaffSubject), @"mvc.1.0.view", @"/Views/Subject/LinkSchoolStaffSubject.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Subject/LinkSchoolStaffSubject.cshtml", typeof(AspNetCore.Views_Subject_LinkSchoolStaffSubject))]
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
#line 1 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\_ViewImports.cshtml"
using Task_2;

#line default
#line hidden
#line 2 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\_ViewImports.cshtml"
using Task_2.Models;

#line default
#line hidden
#line 1 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
using DAL.Entities;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ee4cbd031f8e55539e48b1c14471930752f69fa", @"/Views/Subject/LinkSchoolStaffSubject.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"65fc85d6a52635311063fd20a489463a3b43f9ea", @"/Views/_ViewImports.cshtml")]
    public class Views_Subject_LinkSchoolStaffSubject : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DAL.Entities.Subject>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
#line 3 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
  
    ViewBag.Title = "Edit";

#line default
#line hidden
            BeginContext(86, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 8 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
 using (Html.BeginForm())
{


#line default
#line hidden
            BeginContext(122, 65, true);
            WriteLiteral("    <fieldset>\r\n        <legend>SchoolStaffs</legend>\r\n\r\n        ");
            EndContext();
            BeginContext(188, 33, false);
#line 14 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
   Write(Html.HiddenFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(221, 104, true);
            WriteLiteral("\r\n\r\n        <div class=\"editor-label\"><b>Имя</b></div>\r\n        <div class=\"editor-field\">\r\n            ");
            EndContext();
            BeginContext(326, 36, false);
#line 18 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(362, 81, true);
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"editor-label\"><b>SchoolStaffs</b></div>\r\n");
            EndContext();
#line 22 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
         foreach (SchoolStaff c in ViewBag.SchoolStaffs)
        {

#line default
#line hidden
            BeginContext(512, 62, true);
            WriteLiteral("            <input type=\"checkbox\" name=\"selectedSchoolStaffs\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 574, "\"", 587, 1);
#line 24 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
WriteAttributeValue("", 582, c.Id, 582, 5, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(588, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(591, 124, false);
#line 24 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
                                                                         Write(((IQueryable<SchoolStaffSubject>)ViewBag.SchoolStaffSubjects).Any(x => x.SchoolStaffId == c.Id) ? "checked=\"checked\"" : "");

#line default
#line hidden
            EndContext();
            BeginContext(716, 3, true);
            WriteLiteral(" />");
            EndContext();
            BeginContext(720, 6, false);
#line 24 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
                                                                                                                                                                                                          Write(c.Name);

#line default
#line hidden
            EndContext();
            BeginContext(726, 9, true);
            WriteLiteral(" <br />\r\n");
            EndContext();
#line 25 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
        }

#line default
#line hidden
            BeginContext(746, 96, true);
            WriteLiteral("\r\n        <p>\r\n            <input type=\"submit\" value=\"Save\" />\r\n        </p>\r\n    </fieldset>\r\n");
            EndContext();
#line 31 "c:\Курс ПВТ практика\Задание 12\Task 12\Task 2\Views\Subject\LinkSchoolStaffSubject.cshtml"
}

#line default
#line hidden
            BeginContext(845, 13, true);
            WriteLiteral("\r\n<div>\r\n    ");
            EndContext();
            BeginContext(858, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f4fea7f70e0d4d8abfe9a7f5c835aa09", async() => {
                BeginContext(880, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(896, 8, true);
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DAL.Entities.Subject> Html { get; private set; }
    }
}
#pragma warning restore 1591
