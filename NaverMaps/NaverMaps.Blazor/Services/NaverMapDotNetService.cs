using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace NaverMaps.Blazor.Services
{
    public class NaverMapDotNetService
    {
        public class Point
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        public EventCallback<Point> LocationChanged { get; set; }

        public EventCallback<string> AddressChanged { get; set; }

        [JSInvokable]
        public void OnLocationChanged(double x, double y)
        {
            if (LocationChanged.HasDelegate)
                LocationChanged.InvokeAsync(new Point() { X = x, Y = y });
        }

        [JSInvokable]
        public void OnAddressChanged(string address)
        {
            if (AddressChanged.HasDelegate)
                AddressChanged.InvokeAsync(address);
        }
    }
}

