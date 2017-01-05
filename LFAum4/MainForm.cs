using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace LFAum4
{
    public partial class MainForm : Form
    {
        private BackgroundSolver solver;

        public MainForm()
        {
            InitializeComponent();
            mazeView.MinimumSize = mazeView.Size;
            ClientSize = new Size(boxPath.Right + 12, boxPath.Bottom + 12);
            MinimumSize = Size;

            solver = new BackgroundSolver();
            solver.Solved += s =>
            {
                boxPath.Text = BuildPathString("Algorithm: " + ChosenAlgorithmText() + "\r\nTime Taken: " + s.ElapsedMs.ToString("N0") + " ms", s.Path);

                mazeView.Path = s.Path;
                mazeView.Refresh();

                buttonLoad.Enabled = true;
                buttonSolve.Enabled = true;
            };
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            boxPath.Text = "Solving the maze...";
            buttonLoad.Enabled = false;
            buttonSolve.Enabled = false;
            solver.Solve(mazeView.Maze, ChosenAlgorithm());
        }

        private MazeSolvingAlgoritm ChosenAlgorithm()
        {
            return (radioMouse.Checked) ? MazeSolvingAlgoritm.RandomMouse :
                   (radioWallLeft.Checked) ? MazeSolvingAlgoritm.WallFollowerLeft :
                   (radioWallRight.Checked) ? MazeSolvingAlgoritm.WallFollowerRight :
                   (radioTremaux.Checked) ? MazeSolvingAlgoritm.Tremaux :
                   (radioFlood.Checked) ? MazeSolvingAlgoritm.FloodFill :
                   (radioDijkstra.Checked) ? MazeSolvingAlgoritm.Dijkstra :
                                             MazeSolvingAlgoritm.AStar;
        }

        private string ChosenAlgorithmText()
        {
            return (radioMouse.Checked) ? "Random Mouse" :
                   (radioWallLeft.Checked) ? "Wall Follower (left)" :
                   (radioWallRight.Checked) ? "Wall Follower (right)" :
                   (radioTremaux.Checked) ? "Tremaux" :
                   (radioFlood.Checked) ? "Flood Fill" :
                   (radioDijkstra.Checked) ? "Dijkstra" : "A*";
        }

        private string BuildPathString(string header, GraphPath path)
        {
            StringBuilder sb = new StringBuilder(200);

            sb.AppendLine(header).AppendLine();
            sb.Append("Total cost: ").AppendLine(path.Cost.ToString()).AppendLine();

            int count = path.VertexCount;
            for (int i = 0; i < count; ++i)
            {
                GraphVertex v = path.Vertex(i);
                sb.Append(i.ToString()).Append(":   (").Append(v.X.ToString()).Append(", ").Append(v.Y.ToString()).AppendLine(")");
            }
            return sb.ToString();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Maze Data (.txt)|*.txt" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MazeGraph maze = null;

                    try
                    {
                        maze = MazeReader.FromFile(dialog.FileName);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "IO Error");
                    }
                    finally
                    {
                        if (maze != null)
                        {
                            boxPath.Text = string.Empty;
                            mazeView.Maze = maze;
                            mazeView.Path = null;
                            UpdateUI();
                        }
                    }
                }
            }
        }

        private void UpdateUI()
        {
            mazeView.Size = mazeView.MazeSize;
            mazeView.Refresh();

            boxPath.Left = mazeView.Right + 6;
            boxPath.Height = mazeView.Height;
            ClientSize = new Size(boxPath.Right + 12, boxPath.Bottom + 12);

            buttonSolve.Enabled = true;
            radioMouse.Enabled = (mazeView.Maze.Count <= 256);
            if (!radioMouse.Enabled && radioMouse.Checked)
                radioA.Checked = true;
        }
    }
}
