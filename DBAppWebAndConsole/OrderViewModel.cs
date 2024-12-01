public class OrderViewModel
{
    public int IdOrder { get; set; }
    public int IdMaster { get; set; }
    public List<SparePart> SelectedSpareParts { get; set; } = [];
    public List<Work> SelectedWorks { get; set; } = [];
    public List<Malfunction> SelectedMalfunctions { get; set; } = [];
    public List<SparePart>? SpareParts { get; set; } = [];
    public List<Work>? Works { get; set; } = [];
    public List<Malfunction>? Malfunctions { get; set; } = [];
    public List<Master>? Masters { get; set; }

    public override string ToString()
    {
        return $"{IdOrder}, {IdMaster}, "
            + $"[{string.Join(", ", SelectedSpareParts)}], "
            + $"[{string.Join(", ", SelectedWorks)}], "
            + $"[{string.Join(", ", SelectedMalfunctions)}]";
    }
}