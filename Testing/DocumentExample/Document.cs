namespace DocumentExample;

public class Document
{
    public Document()
    {
        Changes = new Dictionary<int, string>();
        SideAcceptance = new Dictionary<Side, bool>();
    }

    public int Id { get; internal set; }
    public Dictionary<int, string> Changes { get; private set; }
    public Dictionary<Side, bool> SideAcceptance { get; private set; }

    public int AddChange(string content)
    {
        int changeId = Changes.Keys.Last() + 1;
        Changes.Add(changeId, content);
        return changeId;
    }

    public void RemoveChange(int changeId)
    {
        if (Changes.ContainsKey(changeId))
            Changes.Remove(changeId);
        else throw new ChangeDoesNotExist(changeId);
    }

    public void Accept(Side side)
    {
        if (!SideAcceptance.ContainsKey(side))
        {
            SideAcceptance.Add(side, true);
        }
        else throw new DocumentAlreadyAcceptedBySide(side);
    }
}