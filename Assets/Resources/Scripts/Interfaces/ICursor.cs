namespace Assets.Resources.Scripts.Interfaces
{
    internal interface ICursor
    {
        float GrabStrength { get; set; }
        bool IsGrabbing { get; set; }
    }
}