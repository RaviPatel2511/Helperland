#pragma checksum "C:\Users\RAVI\Desktop\Helperland\Helperland\Helperland\Views\Provider\NewService.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13001beae349fda2b57ce26ca166c7dcfd79efbb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Provider_NewService), @"mvc.1.0.view", @"/Views/Provider/NewService.cshtml")]
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
#line 1 "C:\Users\RAVI\Desktop\Helperland\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\RAVI\Desktop\Helperland\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"13001beae349fda2b57ce26ca166c7dcfd79efbb", @"/Views/Provider/NewService.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_Provider_NewService : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/home/window-close-regular.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("closebtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/upcoming_service/arrow.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("sorting-arrow"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/PNewService.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\RAVI\Desktop\Helperland\Helperland\Helperland\Views\Provider\NewService.cshtml"
  
    Layout = "~/Views/Shared/_ProDashLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div id=""NewService"">
    <div class=""modal fade"" id=""AcceptService"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
        <div class=""modal-dialog modal-dialog-centered"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-body text-center"">
                    <input type=""text"" class=""d-none"" id=""acceptSerId"" />
                    <h3>Are you sure you want to Accept service ?</h3>
                    <input type=""button"" class=""btn"" value=""Yes"" onclick=""AcceptService()"" />
                </div>
            </div>
        </div>
    </div>
    <div class=""modal fade"" id=""AcceptServiceError"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
        <div class=""modal-dialog modal-dialog-centered"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-body text-center"">
                    <h3 class=""text-danger"">Sorry! Another provi");
            WriteLiteral(@"der already booked this service ..</h3>
                    <input type=""button"" class=""btn"" value=""Ok"" id=""AcceptServiceErrorBtn"" />
                </div>
            </div>
        </div>
    </div>
    <div>
        <div class=""modal fade"" id=""displaydataModal"" role=""dialog"">
            <div class=""modal-dialog modal-dialog-centered"">
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h4 class=""modal-title"">Service Details</h4>
                        <a class=""close"" data-dismiss=""modal"">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "13001beae349fda2b57ce26ca166c7dcfd79efbb7157", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</a>
                    </div>
                    <div class=""modal-body"">
                        <div id=""SerDetail"">
                            <p class=""date-time""><span id=""SerDate""></span> <span id=""SerStartTime""></span>-<span id=""SerEndTime""></span></p>
                            <span class=""title-text""><b>Duration : </b><span id=""SerDuration""></span> Hrs</span>
                            <hr />
                            <span class=""title-text""><b>Service Id : </b><span id=""SerId""></span></span>
                            <span class=""title-text""><b>Extras :  </b><span id=""SerExtra""></span></span>
                            <span class=""title-text""><b>Net Amout : </b><span id=""SerPayment"" class=""NetAmount""></span></span>
                            <hr />
                            <span class=""title-text""><b>Service Address : </b><span id=""SerAddress""></span></span>
                            <span class=""title-text""><b>Billing Address : </b>Same as cleaning Address</span>
  ");
            WriteLiteral(@"                          <span class=""title-text""><b>Name : </b><span id=""SerCustName""></span></span>
                            <span class=""title-text""><b>Phone : </b><span id=""SerMobile""></span></span>
                            <span class=""title-text""><b>Email : </b><span id=""SerEmail""></span></span>
                            <hr />
                            <span class=""title-text""><b>Comments : </b><span id=""SerComment""></span></span>
                            <span class=""title-text""><span id=""SerPets""></span></span>
                            <hr />
                            <div class=""action-btn"">
                            </div>
                        </div>
                        <div>
                            <div id='CustMap'>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""d-flex datatable_heading"">
        <div class=""py-2"">New");
            WriteLiteral(@" Service Request</div>
        <div class=""py-2 text-center"" id=""havePetFilterDiv""><input class=""mr-2"" type=""checkbox"" id=""havePetFilter"" /> <label class=""mb-0"" for=""havePetFilter"">Include pet at home</label></div>
        <div class=""py-2 ml-auto mobile_sorting"">
            <a href=""#"" data-toggle=""modal"" data-target=""#mobileSorting"" data-dismiss=""modal"">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "13001beae349fda2b57ce26ca166c7dcfd79efbb10828", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</a>

        </div>
        <div class=""modal fade"" id=""mobileSorting"" role=""dialog"">
            <div class=""modal-dialog"">
                <!-- Modal content-->
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h4 class=""modal-title"">Choose Option</h4>
                        <a class=""close"" data-dismiss=""modal"">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "13001beae349fda2b57ce26ca166c7dcfd79efbb12351", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</a>\r\n                    </div>\r\n                    <div class=\"modal-body forgot-password\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "13001beae349fda2b57ce26ca166c7dcfd79efbb13590", async() => {
                WriteLiteral(@"
                            <input type=""radio"" name=""sortOption"" id=""ServiceIdOldest"" value=""ServiceId:Oldest"">&nbsp;Service Id : Oldest<br>
                            <input type=""radio"" name=""sortOption"" id=""ServiceIdOldest"" value=""ServiceId:Latest"">&nbsp;Service Id : Latest<br>
                            <input type=""radio"" name=""sortOption"" id=""ServiceDateOldest"" value=""ServiceDate:Oldest"">&nbsp;Service Date : Oldest<br>
                            <input type=""radio"" name=""sortOption"" id=""ServiceDateLatest"" value=""ServiceDate:Latest"">&nbsp;Service Date : Latest<br>
                            <input type=""radio"" name=""sortOption"" id=""CustomerDetailsAtoZ"" value=""CustomerDetails:AtoZ"">&nbsp;Customer Details: A to Z<br>
                            <input type=""radio"" name=""sortOption"" id=""CustomerDetailsZtoA"" value=""CustomerDetails:ZtoA"">&nbsp;Customer Details: Z to A<br>
                            <input type=""radio"" name=""sortOption"" id=""PaymentLowtoHigh"" value=""PaymentLowtoHigh"">&nbsp;Payment");
                WriteLiteral(" Low to High<br>\r\n                            <input type=\"radio\" name=\"sortOption\" id=\"PaymentHightoLow\" value=\"PaymentHightoLow\">&nbsp;Payment High to Low<br>\r\n\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""row"">
        <div class=""col"">
            <table id=""NewServiceTbl"" class=""dataTable table"">
                <thead>
                    <tr>
                        <th>Service ID</th>
                        <th>Service date</th>
                        <th class=""text-center"">Customer detail</th>
                        <th class=""text-center"">Payment</th>
                        <th class=""text-center"">Time conflict</th>
                        <th class=""text-center"">Actions</th>
                        <th class=""d-none""></th>
                    </tr>
                </thead>
                <tbody id=""NewServiceTblData"">
");
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            DefineSection("script", async() => {
                WriteLiteral("\r\n    <script src=\"https://unpkg.com/leaflet@1.7.1/dist/leaflet.js\"\r\n            integrity=\"sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA==\"");
                BeginWriteAttribute("crossorigin", "\r\n            crossorigin=\"", 8368, "\"", 8395, 0);
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "13001beae349fda2b57ce26ca166c7dcfd79efbb17542", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
