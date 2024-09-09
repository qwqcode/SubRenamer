namespace SubRenamer.Model;

public interface IWindowService
{
     delegate void SetTopmostDelegate(bool topmost);
     
     void SetTopmost(bool val);
}
