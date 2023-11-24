namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public interface IHircItemContainer
{
    public uint Size { get; set; }
    
    public HircItem Item { get; set; }
}