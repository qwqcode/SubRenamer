using System;
using Avalonia.Controls;
using SubRenamer.Model;

namespace SubRenamer.Services;

public class WindowService(Window target, IWindowService.SetTopmostDelegate onSetTopmost) : IWindowService
{
    private readonly Window _target = target;

    public void SetTopmost(bool topmost)
    {
        onSetTopmost(topmost);
    }
}