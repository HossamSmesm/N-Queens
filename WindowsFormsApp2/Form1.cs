using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private int N;
        public Form1()
        {
            InitializeComponent();
        }
        bool IsStrike(int x1, int y1, int x2, int y2)
        {
            if ((x1 == x2) || (y1 == y2))
                return true;

            int tx, ty;
            tx = x1 - 1;
            ty = y1 - 1;

            while ((tx >= 1) && (ty >= 1))
            {
                if ((tx == x2) && (ty == y2))
                    return true;

                tx--;
                ty--;
            }

            tx = x1 + 1;
            ty = y1 + 1;

            while ((tx <= N) && (ty <= N))
            {
                if ((tx == x2) && (ty == y2))
                    return true;

                tx++;
                ty++;
            }

            tx = x1 + 1;
            ty = y1 - 1;

            while ((tx <= N) && (ty >= 1))
            {
                if ((tx == x2) && (ty == y2))
                    return true;

                tx++;
                ty--;
            }

            tx = x1 - 1;
            ty = y1 + 1;

            while ((tx >= 1) && (ty <= N))
            {
                if ((tx == x2) && (ty == y2))
                    return true;

                tx--;
                ty++;
            }
            return false;
        }
        bool Strike(int[] M, int p)
        {
            int px, py, x, y;
            int i;

            px = M[p];
            py = p;

            for (i = 1; i <= p - 1; i++)
            {
                x = M[i];
                y = i;

                if (IsStrike(x, y, px, py))
                    return true;
            }
            return false;
        }
        void InitDataGridView(int N)
        {
            int i;

            dataGridView1.Columns.Clear();

            for (i = 1; i <= N; i++)
            {
                dataGridView1.Columns.Add("i" + i.ToString(), i.ToString());
                dataGridView1.Columns[i - 1].Width = 30;
            }

            dataGridView1.Rows.Add(N);

            for (i = 1; i <= N; i++)
                dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        void ShowDataGridView(string s, int N)
        {
            int i;
            int j;
            string xs, ys;
            int x, y;

            for (i = 0; i < N; i++)
                for (j = 0; j < N; j++)
                    dataGridView1.Rows[i].Cells[j].Value = "";
            j = 0;

            for (i = 0; i < N; i++)
            {
                xs = "";

                while (s[j] != ',')
                {
                    xs = xs + s[j].ToString();
                    j++;
                }

                j++;
                ys = "";

                while (s[j] != '-')
                {
                    ys = ys + s[j].ToString();
                    j++;
                }

                j++;
                x = Convert.ToInt32(xs);
                y = Convert.ToInt32(ys);
                dataGridView1.Rows[y - 1].Cells[x - 1].Value = "X";
            }
        }
        void AddToListBox(int[] M, int N)
        {
            string s = "";

            for (int i = 1; i <= N; i++)
                s = s + M[i].ToString() + "," + i.ToString() + "-";

            listBox1.Items.Add(s);
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 0) return;
            int num;
            string s;
            num = listBox1.SelectedIndex;
            s = listBox1.Items[num].ToString();
            ShowDataGridView(s, N);
        }
        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (listBox1.Items.Count <= 0) return;
            int num;
            string s;
            num = listBox1.SelectedIndex;
            s = listBox1.Items[num].ToString();
            ShowDataGridView(s, N);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const int MaxN = 20;
            int[] M = new int[MaxN]; // the array of placements
            int p; // queen number
            int k; // number of placement options

            // get the number of queens
            N = Convert.ToInt32(textBox1.Text);

            // Initialize dataGridView1
            InitDataGridView(N);

            // clear listBox1
            listBox1.Items.Clear();

            // THE ALGORITHM FOR GENERATING PLACEMENTS
            // initial settings
            p = 1;
            M[p] = 0;
            M[0] = 0;
            k = 0;

            // the cycle of placements search
            while (p > 0) // if p==0, then exit from the loop
            {
                M[p] = M[p] + 1;
                if (p == N) // last item
                {
                    if (M[p] > N)
                    {
                        while (M[p] > N) p--; // rewind
                    }
                    else
                    {
                        if (!Strike(M, p))
                        {
                            // fix placement
                            AddToListBox(M, N);
                            k++;
                            p--;
                        }
                    }
                }
                else // not the last item
                {
                    if (M[p] > N)
                    {
                        while (M[p] > N) p--; // rewind
                    }
                    else
                    {
                        if (!Strike(M, p)) // If M[p] does not overlap with previous M[1], M[2], ..., M[p-1]
                        {
                            p++; // go to the placement of the next queen
                            M[p] = 0;
                        }
                    }
                }
            }

            // display the number of placement options
            if (k > 0)
            {
                listBox1.SelectedIndex = 0;
                listBox1_Click(sender, e);
                label2.Text = "Number of placements = " + k.ToString();
            }
        }
    }
}

