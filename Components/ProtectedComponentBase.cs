using go_han_fe.Services.State;
using Microsoft.AspNetCore.Components;

namespace go_han_fe.Components
{
    public class ProtectedComponentBase : LayoutComponentBase
    {
        [Inject] protected NavigationManager Navigation { get; set; } = null!;
        [Inject] protected AuthState AuthState { get; set; } = null!;

        private bool _checkedAuth = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender || _checkedAuth)
                return;

            _checkedAuth = true;

            // Kalau auth belum siap, tunggu sekali saja
            if (!AuthState.IsLoaded)
            {
                await AuthState.WaitUntilLoadedAsync();
            }

            if (!AuthState.IsAuthenticated)
            {
                Navigation.NavigateTo("/login", true);
            }
        }
    }
}
