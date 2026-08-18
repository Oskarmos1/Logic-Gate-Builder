using Logic_Gate_Builder;

public interface IGate
{
    void execute();
    int getNumberOfInputs();
    string getGateType();
    void removeInput();
    string getName();
    MyList<string> returnGateInfo();
    void resetGate();

    void setName(string newName);

    IGate exportComponent();

    int getGateNum();
    
}