namespace LFAum4
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupAlgorithms = new System.Windows.Forms.GroupBox();
            this.radioA = new System.Windows.Forms.RadioButton();
            this.radioDijkstra = new System.Windows.Forms.RadioButton();
            this.radioFlood = new System.Windows.Forms.RadioButton();
            this.radioTremaux = new System.Windows.Forms.RadioButton();
            this.radioWallRight = new System.Windows.Forms.RadioButton();
            this.radioWallLeft = new System.Windows.Forms.RadioButton();
            this.radioMouse = new System.Windows.Forms.RadioButton();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.boxPath = new System.Windows.Forms.TextBox();
            this.mazeView = new LFAum4.MazePanel();
            this.groupAlgorithms.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 12);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(127, 23);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "LOAD MAZE DATA";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // groupAlgorithms
            // 
            this.groupAlgorithms.Controls.Add(this.radioA);
            this.groupAlgorithms.Controls.Add(this.radioDijkstra);
            this.groupAlgorithms.Controls.Add(this.radioFlood);
            this.groupAlgorithms.Controls.Add(this.radioTremaux);
            this.groupAlgorithms.Controls.Add(this.radioWallRight);
            this.groupAlgorithms.Controls.Add(this.radioWallLeft);
            this.groupAlgorithms.Controls.Add(this.radioMouse);
            this.groupAlgorithms.Location = new System.Drawing.Point(12, 41);
            this.groupAlgorithms.Name = "groupAlgorithms";
            this.groupAlgorithms.Size = new System.Drawing.Size(127, 189);
            this.groupAlgorithms.TabIndex = 2;
            this.groupAlgorithms.TabStop = false;
            this.groupAlgorithms.Text = "Algorithms:";
            // 
            // radioA
            // 
            this.radioA.AutoSize = true;
            this.radioA.Checked = true;
            this.radioA.Location = new System.Drawing.Point(6, 157);
            this.radioA.Name = "radioA";
            this.radioA.Size = new System.Drawing.Size(36, 17);
            this.radioA.TabIndex = 6;
            this.radioA.TabStop = true;
            this.radioA.Text = "A*";
            this.radioA.UseVisualStyleBackColor = true;
            // 
            // radioDijkstra
            // 
            this.radioDijkstra.AutoSize = true;
            this.radioDijkstra.Location = new System.Drawing.Point(6, 134);
            this.radioDijkstra.Name = "radioDijkstra";
            this.radioDijkstra.Size = new System.Drawing.Size(60, 17);
            this.radioDijkstra.TabIndex = 5;
            this.radioDijkstra.Text = "Dijkstra";
            this.radioDijkstra.UseVisualStyleBackColor = true;
            // 
            // radioFlood
            // 
            this.radioFlood.AutoSize = true;
            this.radioFlood.Location = new System.Drawing.Point(6, 111);
            this.radioFlood.Name = "radioFlood";
            this.radioFlood.Size = new System.Drawing.Size(66, 17);
            this.radioFlood.TabIndex = 4;
            this.radioFlood.Text = "Flood Fill";
            this.radioFlood.UseVisualStyleBackColor = true;
            // 
            // radioTremaux
            // 
            this.radioTremaux.AutoSize = true;
            this.radioTremaux.Location = new System.Drawing.Point(6, 88);
            this.radioTremaux.Name = "radioTremaux";
            this.radioTremaux.Size = new System.Drawing.Size(66, 17);
            this.radioTremaux.TabIndex = 3;
            this.radioTremaux.Text = "Tremaux";
            this.radioTremaux.UseVisualStyleBackColor = true;
            // 
            // radioWallRight
            // 
            this.radioWallRight.AutoSize = true;
            this.radioWallRight.Location = new System.Drawing.Point(6, 65);
            this.radioWallRight.Name = "radioWallRight";
            this.radioWallRight.Size = new System.Drawing.Size(117, 17);
            this.radioWallRight.TabIndex = 2;
            this.radioWallRight.Text = "Wall Follower (right)";
            this.radioWallRight.UseVisualStyleBackColor = true;
            // 
            // radioWallLeft
            // 
            this.radioWallLeft.AutoSize = true;
            this.radioWallLeft.Location = new System.Drawing.Point(6, 42);
            this.radioWallLeft.Name = "radioWallLeft";
            this.radioWallLeft.Size = new System.Drawing.Size(111, 17);
            this.radioWallLeft.TabIndex = 1;
            this.radioWallLeft.Text = "Wall Follower (left)";
            this.radioWallLeft.UseVisualStyleBackColor = true;
            // 
            // radioMouse
            // 
            this.radioMouse.AutoSize = true;
            this.radioMouse.Location = new System.Drawing.Point(6, 19);
            this.radioMouse.Name = "radioMouse";
            this.radioMouse.Size = new System.Drawing.Size(100, 17);
            this.radioMouse.TabIndex = 0;
            this.radioMouse.Text = "Random Mouse";
            this.radioMouse.UseVisualStyleBackColor = true;
            // 
            // buttonSolve
            // 
            this.buttonSolve.Enabled = false;
            this.buttonSolve.Location = new System.Drawing.Point(33, 236);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(85, 23);
            this.buttonSolve.TabIndex = 3;
            this.buttonSolve.Text = "SOLVE";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // boxPath
            // 
            this.boxPath.BackColor = System.Drawing.Color.White;
            this.boxPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxPath.Location = new System.Drawing.Point(398, 12);
            this.boxPath.Multiline = true;
            this.boxPath.Name = "boxPath";
            this.boxPath.ReadOnly = true;
            this.boxPath.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.boxPath.Size = new System.Drawing.Size(192, 247);
            this.boxPath.TabIndex = 4;
            this.boxPath.WordWrap = false;
            // 
            // mazeView
            // 
            this.mazeView.BackColor = System.Drawing.Color.White;
            this.mazeView.Location = new System.Drawing.Point(145, 12);
            this.mazeView.Maze = null;
            this.mazeView.Name = "mazeView";
            this.mazeView.Path = null;
            this.mazeView.Size = new System.Drawing.Size(247, 247);
            this.mazeView.TabIndex = 0;
            this.mazeView.TileSize = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 273);
            this.Controls.Add(this.boxPath);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.groupAlgorithms);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.mazeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "LF AUM 4";
            this.groupAlgorithms.ResumeLayout(false);
            this.groupAlgorithms.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MazePanel mazeView;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.GroupBox groupAlgorithms;
        private System.Windows.Forms.RadioButton radioWallRight;
        private System.Windows.Forms.RadioButton radioWallLeft;
        private System.Windows.Forms.RadioButton radioMouse;
        private System.Windows.Forms.RadioButton radioTremaux;
        private System.Windows.Forms.RadioButton radioA;
        private System.Windows.Forms.RadioButton radioDijkstra;
        private System.Windows.Forms.RadioButton radioFlood;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.TextBox boxPath;
    }
}

