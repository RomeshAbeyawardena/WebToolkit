using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class HtmlMinifyTests
    {
        [Fact]
        public void HtmlMinify_Minify()
        {
            var source = @"<div class=""landing__column landing__column--padded/"">
            <h1 class=""landing__title app-section__title"">welcome to the customer portal</h1>

            <p>
                Lorem ipsum dolor sit amet consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna
            </p>
            <img src=""~/Content/images/mobile-responsive-design.png"" class=""landing__hero-image"" />
        </div>
        <div class=""landing__column"">
            <div class=""landing__form app-card"">
                <form>
                    <h3 class=""app-form__title app-section__title"">activate your account</h3>

                    <div class=""app-form__group"">
                        <label class=""app-form__label"" for=""firstName"">First Name</label>
                        <input class=""app-form__input"" data-val=""true"" data-val-regex=""Please enter a valid phone number"" id=""firstName"" name=""firstName"" type=""text"" value="""">
                        <span class=""field-validation-valid""></span>
                    </div>
                    <div class=""app-form__group"">
                        <label class=""app-form__label"" for=""surname"">Surname</label>
                        <input class=""app-form__input"" data-val=""true"" data-val-regex=""Please enter a valid phone number"" id=""surname"" name=""surname"" type=""text"" value="""">
                        <span class=""field-validation-valid""></span>
                    </div>
                    <div class=""app-form__group"">
                        <label class=""app-form__label"" for=""email"">Email Address</label>
                        <input class=""app-form__input"" data-val=""true"" data-val-regex=""Please enter a valid phone number"" id=""email"" name=""email"" type=""email"" value="""">
                        <span class=""field-validation-valid""></span>
                    </div>
                    <div class=""app-form__group"">
                        <label class=""app-form__label"" for=""password"">Email Address</label>
                        <input class=""app-form__input"" data-val=""true"" data-val-regex=""Please enter a valid phone number"" id=""password"" name=""password"" type=""password"" value="""">
                        <span class=""field-validation-valid""></span>
                    </div>

                    <div class=""button-group"">
                        <input type=""submit"" class=""button button-group__button button--primary"" value=""register to access your portal"" />
                    </div>
                </form>
            </div>
        </div>";
            var minifiedSource = HtmlMinify.Minify(source);
            //Assert.False(HtmlMinify.MinifyableValues.All(mv =>  minifiedSource.Contains(mv) ) );
        }
    }
}