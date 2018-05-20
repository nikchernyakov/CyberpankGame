public class ObjectShower : DonebleChecker {

    public Showable target;

    public override void UpdateDone(Doneble donebleObject)
    {
        base.UpdateDone(donebleObject);

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
