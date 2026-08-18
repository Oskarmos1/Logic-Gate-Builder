
using Logic_Gate_Builder;
using Logic_Gate_Builder.UI_Classes;
using System.Diagnostics;
using System.Windows.Forms;

public class CanvasPanel : Panel
{
    private bool isDrawing = false;
    private Point currentSourcePoint;
    private Point mousePos;
    private MyList<Connection> connectionList = new MyList<Connection>();
    public CanvasPanel()
    {
        this.DoubleBuffered = true;
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (Pen pen = new Pen(Color.Black, 2))
        {
            for (int i = 0; i < connectionList.getLength(); i++)
            {
                Connection connection = connectionList.getItem(i);
                e.Graphics.DrawLine(pen, connection.getSourceP(), connection.getTargetP());
            }
            if (isDrawing == true)
            {
                e.Graphics.DrawLine(pen, currentSourcePoint, mousePos);
            }
        }
    }

    public bool getIsDrawing()
    {
        return isDrawing;
    }

    public void setIsDrawing(bool iD)
    {
        this.isDrawing = iD;
    }

    public Point getCurrentSourcePoint() { 
        return currentSourcePoint;
    }

    public void setCurrentSourcePoint(Point cSP) { 
        this.currentSourcePoint = cSP;
    }

    public Point getMousePos() {
        return mousePos;
    }

    public void setMousePos(Point mP) { 
        this.mousePos = mP;
    }

    public MyList<Connection> getConnectionList() { 
        return connectionList;
    }

}