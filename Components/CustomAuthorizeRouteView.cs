// Components/CustomAuthorizeRouteView.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using go_han_fe.Services.State;

namespace go_han_fe.Components
{
    public class CustomAuthorizeRouteView : RouteView
    {
        private NavigationManager Navigation { get; set; } = null!;

        private AuthState AuthState { get; set; } = null!;

        protected override void Render(RenderTreeBuilder builder)
        {
            // Gunakan tipe penuh untuk menghindari ambiguitas
            var pageType = RouteData.PageType;
            var authorize = Attribute.GetCustomAttribute(pageType, typeof(AuthorizeAttribute)) != null;

            if (authorize && !AuthState.IsAuthenticated)
            {
                // Jika halaman butuh login tapi belum login
                var currentUri = Navigation.Uri;
                var escapedUri = Uri.EscapeDataString(currentUri);
                Navigation.NavigateTo($"/login?returnUrl={escapedUri}", true);
                return;
            }

            base.Render(builder);
        }
    }
}