/*
 * Script that Show or Hide object when Doneble event is happened
 */ 
public class ObjectShower : DonebleChecker {

    public Showable target;

    public override void UpdateDone(Doneble donebleObject)
    {
        if (CheckDonebleList(true))
        {
            ShowObject();
        }
        else
        {
            target.Hide();
        }
    }

    public void ShowObject()
    {
        target.Show();
    }
}
