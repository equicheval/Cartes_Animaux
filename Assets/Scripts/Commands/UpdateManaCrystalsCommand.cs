using UnityEngine;
using System.Collections;

public class UpdateManaCrystalsCommand : Command {

    private Player p;

    private int AvailableOceanRessources;
    private int AvailableSunRessources;

    public UpdateManaCrystalsCommand(Player p, int AvailableSunRessources, int AvailableOceanRessources)
    {
        this.p = p;
        this.AvailableOceanRessources = AvailableOceanRessources;
        this.AvailableSunRessources = AvailableSunRessources;
    }

    public override void StartCommandExecution()
    {
        p.PArea.ManaBar.AvailableOceanRessources = AvailableOceanRessources;
        p.PArea.ManaBar.AvailableSunRessources = AvailableSunRessources;
        CommandExecutionComplete();
    }
}
