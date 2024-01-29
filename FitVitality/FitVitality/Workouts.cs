﻿using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitVitality
{
    public partial class Workouts : Form
    {
        public string _userID;

        // Булеви променливи, които отбелязват къде ще се изпълнява тренировката
        bool gymClicked = false;
        bool homeClicked = false;
        bool outdoorsClicked = false;

        // Булеви променливи, които отбелязват кои части от тялото ще се тренират
        bool upperBodyClicked = false;
        bool coreClicked = false;
        bool lowerBodyClicked = false;

        // Булеви променливи, които отбелязват дали е натиснато да или не
        bool yesClicked = false;
        bool noClicked = false;

        // Допълнителни булеви променливи, които отбелязват какъв тип тренировка ще се изпълнява
        bool cardioClicked = false;
        string name = "";

        // Булеви променливи, които отбелязват дали е натиснато готова тренировка или създаване на собствена
        bool preClicked = false;
        bool createClicked = false;

        string workoutPlace = "";
        int activity;
        int workoutNumber;

        // Булеви променливи, които отбелязват кой ден от седмицата е натиснат
        bool mondayClicked = false;
        bool tuesdayClicked = false;
        bool wednesdayClicked = false;
        bool thursdayClicked = false;
        bool fridayClicked = false;
        bool saturdayClicked = false;
        bool sundayClicked = false;

        bool workoutDaysDone = false;
        string workoutSelection = "";

        int selectedDays = 0;

        //Exercises list

        //Gym
        List<string> gymBack = new List<string> { "Deadlift", "Pull-ups", "Bent-over row", "Lat pulldown", "Seated cable row", "T-bar row", "One-arm dumbbell row", "Inverted row", "Single arm lat pulldown" };
        List<string> gymBiceps = new List<string> { "Barbell curl", "Dumbbell curl", "Hammer curl", "Preacher curl", "Cable curl", "EZ bar curl", "Incline dumbbell curl" };
        List<string> gymChest = new List<string> { "Bench press", "Dumbbell bench press", "Incline bench press", "Incline dumbbell bench press", "Decline bench press", "Decline dumbbell bench press", "Chest fly", "Incline chest fly", "Decline chest fly", "Cable fly", "Incline cable fly", "Decline cable fly", "Dips", "Incline dumbbell fly", "Decline dumbbell fly" };
        List<string> gymTriceps = new List<string> { "Triceps pushdown", "Overhead triceps extension", "Skullcrusher", "Triceps kickback", "Cable overhead triceps extension", "Triceps rope pushdown" };
        List<string> gymShoulders = new List<string> { "Overhead press", "Dumbbell overhead press", "Dumbbell lateral raise", "Front raise", "Rear delt fly", "Face pull" };
        List<string> gymQuadriceps = new List<string> { "Squat", "Leg press", "Leg extension", "Hack squat", "Front squat", "Bulgarian split squat", "Lunge" };
        List<string> gymHamstrings = new List<string> { "Romanian deadlift", "Leg curl", "Lying leg curl", "Good morning" };
        List<string> gymCalves = new List<string> { "Standing calf raise", "Seated calf raise" };
        List<string> gymGlutes = new List<string> { "Hip thrust", "Glute bridge", "Barbell glute bridge" };
        List<string> gymCore = new List<string> { "Hanging leg raises", "Hanging side-to-side raises", "Cable oblique crunches", "Weighted oblique crunches" };

        //
        List<string> homeBackBiceps = new List<string> { "Towel pull-ups", "Iron man flyes", "Boat lifts", "Butterflyes", "Rocking leafs", "Bodyweight lat pulldowns", "Bodyweight rows", "Straight arm pulldown", "Bed sheet face pulls", "Prone arm circles" };
        List<string> homeChestTricepsShoulders = new List<string> { "Diamond push-ups", "Pike push-ups", "Decline push-ups", "Handstand push-ups", "Dips", "Triceps dips", "Triceps push-ups", "Triceps bench dips", "Triceps floor dips" };
        List<string> homeLegs = new List<string> { "Squats", "Pistol squats", "Lunges", "Bulgarian split squats", "Jumping lunges", "Jumping squats", "Calf raises", "Glute bridges", "Single leg glute bridges", "Wall sit" };
        List<string> homeCore = new List<string> { "Plank", "Side plank", "Reverse plank", "Crunches", "Bicycle crunches", "Flutter kicks", "Russian twists", "V-ups", "Sit-ups", "Mountain climbers", "Burpees", "Jumping jacks", "High knees", "Plank jacks", "Scissor kicks" };

        //Outdoors
        List<string> outdoorsBackBiceps = new List<string> { "Pull-ups", "Chin-ups", "Inverted rows", "Australian pull-ups", "Bicep bar curls" };
        List<string> outdoorsChestTricepsShoulders = new List<string> { "Push-ups", "Decline push-ups", "Diamond push-ups", "Pike push-ups", "Handstand push-ups", "Dips", "Triceps dips", "Triceps push-ups" };
        List<string> outdoorsLegs = new List<string> { "Squats", "Lunges", "Bulgarian split squats", "Jumping lunges", "Jumping squats", "Calf raises", "Glute bridges", "Single leg glute bridges", "Wall sit" };
        List<string> outdoorsCore = new List<string> { "Plank", "Side plank", "Reverse plank", "Crunches", "Bicycle crunches", "Flutter kicks", "Russian twists", "V-ups", "Sit-ups", "Mountain climbers", "Burpees", "Jumping jacks", "High knees", "Plank jacks", "Scissor kicks", "Toes to bar", "Hanging knee raises", "V-raises" };

        List<WorkoutListItem> workouts = new List<WorkoutListItem>();

        List<string> backbiceps = new List<string>();
        List<string> chesttricepsshoulders = new List<string>();
        List<string> legs = new List<string>();
        List<string> _core = new List<string>();

        List<string> back = new List<string>();
        List<string> biceps = new List<string>();
        List<string> chest = new List<string>();
        List<string> triceps = new List<string>();
        List<string> shoulders = new List<string>();
        List<string> quadriceps = new List<string>();
        List<string> hamstrings = new List<string>();
        List<string> glutes = new List<string>();
        List<string> calves = new List<string>();
        List<string> core = new List<string>();
        //private string connectionString = @"Server=tcp:fitvitality.database.windows.net,1433;Initial Catalog=FitVitality-AWS;Persist Security Info=False;User ID=Member;Password=useraccessPass1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public Workouts(string userID)
        {
            InitializeComponent();
            _userID = userID;


        }
        private void Workouts_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Name FROM UserSettings WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", _userID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader["Name"].ToString();
                        }
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Workouts WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", _userID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                if (reader["Monday"].ToString() != "Rest Day" || reader["Tuesday"].ToString() != "Rest Day" ||
                                    reader["Wednesday"].ToString() != "Rest Day" || reader["Thursday"].ToString() != "Rest Day" ||
                                    reader["Friday"].ToString() != "Rest Day" || reader["Saturday"].ToString() != "Rest Day" ||
                                    reader["Sunday"].ToString() != "Rest Day")
                                {
                                    workoutsDashboard.Visible = true;
                                    workoutsDashboard.BringToFront();
                                }
                            }
                        }
                    }
                }
            }
            trainPlaceLabel.Text = $"Hello, {name}. This is the workout section and we will \r\nask you a few questions in order to create the \r\nperfect workout for you.\r\nTo begin with, what place do you desire to workout at?\r\n";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void gymButton_Click(object sender, EventArgs e)
        {
            gymButton.Image = Properties.Resources.gymPressed;
            homeButton.Image = Properties.Resources.home1;
            outdoorsButton.Image = Properties.Resources.outdoors;
            gymClicked = true;
            homeClicked = false;
            outdoorsClicked = false;
            nextButton1.Visible = true;
            workoutPlace = "Gym";
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            gymButton.Image = Properties.Resources.gym;
            homeButton.Image = Properties.Resources.homePressed;
            outdoorsButton.Image = Properties.Resources.outdoors;
            gymClicked = false;
            homeClicked = true;
            outdoorsClicked = false;
            nextButton1.Visible = true;
            workoutPlace = "Home";
        }

        private void outdoorsButton_Click(object sender, EventArgs e)
        {
            gymButton.Image = Properties.Resources.gym;
            homeButton.Image = Properties.Resources.home1;
            outdoorsButton.Image = Properties.Resources.outdoorsPressed;
            gymClicked = false;
            homeClicked = false;
            outdoorsClicked = true;
            nextButton1.Visible = true;
            workoutPlace = "Outdoors";
        }

        private void gymButton_MouseEnter(object sender, EventArgs e)
        {
            if (!gymClicked)
            {
                gymButton.Image = Properties.Resources.gymHover;
            }
        }

        private void gymButton_MouseLeave(object sender, EventArgs e)
        {
            if (!gymClicked)
            {
                gymButton.Image = Properties.Resources.gym;
            }
        }

        private void homeButton_MouseEnter(object sender, EventArgs e)
        {
            if (!homeClicked)
            {
                homeButton.Image = Properties.Resources.homeHover;
            }
        }

        private void homeButton_MouseLeave(object sender, EventArgs e)
        {
            if (!homeClicked)
            {
                homeButton.Image = Properties.Resources.home1;
            }
        }

        private void outdoorsButton_MouseEnter(object sender, EventArgs e)
        {
            if (!outdoorsClicked)
            {
                outdoorsButton.Image = Properties.Resources.outdoorsHovered;
            }
        }

        private void outdoorsButton_MouseLeave(object sender, EventArgs e)
        {
            if (!outdoorsClicked)
            {
                outdoorsButton.Image = Properties.Resources.outdoors;
            }
        }

        private void nextButton1_Click(object sender, EventArgs e)
        {
            trainPlacePanel.Visible = false;
            activityGroupPanel.Visible = true;
        }

        private void muscleGroupsBorders_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string query = "UPDATE UserSettings SET ActivityLevel = @ActivityLevel WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ActivityLevel", activity);
                command.Parameters.AddWithValue("@UserID", _userID);
                command.ExecuteNonQuery();
            }
            workoutTypePanel.Visible = true;
            activityGroupPanel.Visible = false;
        }

        private void activityLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (activityLevelComboBox.SelectedIndex)
            {
                case 0:
                    activity = 1;
                    nextButton2.Visible = true;
                    break;
                case 1:
                    activity = 2;
                    nextButton2.Visible = true;
                    break;
                case 2:
                    activity = 3;
                    nextButton2.Visible = true;
                    break;
                case 3:
                    activity = 4;
                    nextButton2.Visible = true;
                    break;
                case 4:
                    activity = 5;
                    nextButton2.Visible = true;
                    break;
                case 5:
                    activity = 6;
                    nextButton2.Visible = true;
                    break;
                case 6:
                    activity = 7;
                    nextButton2.Visible = true;
                    break;
                default:
                    nextButton2.Visible = false;
                    break;
            }
            upToDaysLabel.Text = $"You must select {activity} days.";
        }

        private void upperButton_Click(object sender, EventArgs e)
        {
            if (upperBodyClicked)
            {
                upperBodyClicked = false;
                upperButton.Image = Properties.Resources.upper;
            }
            else
            {
                upperBodyClicked = true;
                upperButton.Image = Properties.Resources.upperPressed;
            }

            if (upperBodyClicked || lowerBodyClicked || coreClicked)
            {
                nextButton3.Visible = true;
            }
            else
            {
                nextButton3.Visible = false;
            }
        }

        private void lowerButton_Click(object sender, EventArgs e)
        {
            if (lowerBodyClicked)
            {
                lowerBodyClicked = false;
                lowerButton.Image = Properties.Resources.lower;
            }
            else
            {
                lowerBodyClicked = true;
                lowerButton.Image = Properties.Resources.lowerPressed;
            }

            if (upperBodyClicked || lowerBodyClicked || coreClicked)
            {
                nextButton3.Visible = true;
            }
            else
            {
                nextButton3.Visible = false;
            }
        }

        private void coreButton_Click(object sender, EventArgs e)
        {
            if (coreClicked)
            {
                coreClicked = false;
                coreButton.Image = Properties.Resources.core;
            }
            else
            {
                coreClicked = true;
                coreButton.Image = Properties.Resources.corePressed;
            }

            if (upperBodyClicked || lowerBodyClicked || coreClicked)
            {
                nextButton3.Visible = true;
            }
            else
            {
                nextButton3.Visible = false;
            }
        }

        private void upperButton_MouseEnter(object sender, EventArgs e)
        {
            upperButton.Image = Properties.Resources.upperHover;
        }

        private void upperButton_MouseLeave(object sender, EventArgs e)
        {
            if (!upperBodyClicked)
            {
                upperButton.Image = Properties.Resources.upper;
            }
            else
            {
                upperButton.Image = Properties.Resources.upperPressed;
            }
        }

        private void lowerButton_MouseEnter(object sender, EventArgs e)
        {
            lowerButton.Image = Properties.Resources.lowerHover;
        }

        private void lowerButton_MouseLeave(object sender, EventArgs e)
        {
            if (!lowerBodyClicked)
            {
                lowerButton.Image = Properties.Resources.lower;
            }
            else
            {
                lowerButton.Image = Properties.Resources.lowerPressed;
            }
        }

        private void coreButton_MouseEnter(object sender, EventArgs e)
        {
            coreButton.Image = Properties.Resources.coreHovered;
        }

        private void coreButton_MouseLeave(object sender, EventArgs e)
        {
            if (!coreClicked)
            {
                coreButton.Image = Properties.Resources.core;
            }
            else
            {
                coreButton.Image = Properties.Resources.corePressed;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nextButton3_Click(object sender, EventArgs e)
        {
            if (preClicked)
            {
                workoutsList.Visible = true;
                workoutsList.BringToFront();
            }
            else if (createClicked)
            {
                muscleGroupPanel.Visible = false;
                if (gymClicked && upperBodyClicked)
                    upperBodyPanel.Visible = true;
                else if (homeClicked || outdoorsClicked && upperBodyClicked)
                {
                    upperBodyHomeOutdoorsPanel.Visible = true;
                }
                if (gymClicked && lowerBodyClicked && !upperBodyClicked)
                    lowerBodyPanel.Visible = true;
                else if ((homeClicked || outdoorsClicked) && lowerBodyClicked && !upperBodyClicked)
                {
                    chooseWorkoutNumberPanel.Visible = true;
                }
                if (!upperBodyClicked && !lowerBodyClicked && coreClicked)
                {
                    chooseWorkoutNumberPanel.Visible = true;
                }
            }
        }

        private void shouldersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (shouldersCheckBox.Checked)
            {
                shouldersPicture.Image = Properties.Resources.shouldersSelected;
            }
            else
            {
                shouldersPicture.Image = Properties.Resources.shoulders;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void chestCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chestCheckBox.Checked)
            {
                chestPicture.Image = Properties.Resources.chestSelected;
            }
            else
            {
                chestPicture.Image = Properties.Resources.chest;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void armsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (armsCheckBox.Checked)
            {
                armsPicture.Image = Properties.Resources.armsSelected;
            }
            else
            {
                armsPicture.Image = Properties.Resources.arms;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void backCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (backCheckBox.Checked)
            {
                backPicture.Image = Properties.Resources.backSelected;
            }
            else
            {
                backPicture.Image = Properties.Resources.back;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void nextButton4_Click(object sender, EventArgs e)
        {
            if (lowerBodyClicked)
            {
                lowerBodyPanel.Visible = true;
                upperBodyPanel.Visible = false;
            }
            else
            {
                upperBodyPanel.Visible = false;
                chooseWorkoutNumberPanel.Visible = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (quadricepsCheckBox.Checked)
            {
                quadricepsPicture.Image = Properties.Resources.quadricepsSelected;
            }
            else
            {
                quadricepsPicture.Image = Properties.Resources.quadriceps;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (hamstringsCheckBox.Checked)
            {
                hamstringsPicture.Image = Properties.Resources.hamstringsSelected;
            }
            else
            {
                hamstringsPicture.Image = Properties.Resources.hamstrings;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (calvesCheckBox.Checked)
            {
                calvesPicture.Image = Properties.Resources.calvesSelected;
            }
            else
            {
                calvesPicture.Image = Properties.Resources.calves;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (glutesCheckBox.Checked)
            {
                glutesPicture.Image = Properties.Resources.glutesSelected;
            }
            else
            {
                glutesPicture.Image = Properties.Resources.glutes;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void nextButton5_Click(object sender, EventArgs e)
        {
            lowerBodyPanel.Visible = false;
            upperBodyPanel.Visible = false;
            chooseWorkoutNumberPanel.Visible = true;
        }





        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }







        private void nextButton6_Click(object sender, EventArgs e)
        {
            lowerBodyPanel.Visible = false;
            upperBodyPanel.Visible = false;
            chooseWorkoutNumberPanel.Visible = true;
            workoutsLabel.Visible = false;
        }

        private void prePicture_Click(object sender, EventArgs e)
        {
            prePicture.Image = Properties.Resources.prePressed;
            createPicture.Image = Properties.Resources.create;
            preClicked = true;
            createClicked = false;
            nextButt7.Visible = true;
        }

        private void createPicture_Click(object sender, EventArgs e)
        {
            prePicture.Image = Properties.Resources.pre;
            createPicture.Image = Properties.Resources.createSelected;
            preClicked = false;
            createClicked = true;
            nextButt7.Visible = true;
        }

        private void prePicture_MouseEnter(object sender, EventArgs e)
        {
            if (!preClicked)
            {
                prePicture.Image = Properties.Resources.preHovered;
            }
        }

        private void prePicture_MouseLeave(object sender, EventArgs e)
        {
            if (!preClicked)
            {
                prePicture.Image = Properties.Resources.pre;
            }
        }

        private void createPicture_MouseEnter(object sender, EventArgs e)
        {
            if (!createClicked)
            {
                createPicture.Image = Properties.Resources.createHovered;
            }
        }

        private void createPicture_MouseLeave(object sender, EventArgs e)
        {
            if (!createClicked)
            {
                createPicture.Image = Properties.Resources.create;
            }
        }

        private void nextButton7_Click(object sender, EventArgs e)
        {
        }

        private void nextButt7_Click(object sender, EventArgs e)
        {
            workoutTypePanel.Visible = false;
            lowerBodyPanel.Visible = false;
            upperBodyPanel.Visible = false;
            chooseWorkoutNumberPanel.Visible = false;
            if (preClicked)
            {
                workoutsList.Visible = true;
            }
            else if (createClicked)
            {
                muscleGroupPanel.Visible = true;
            }
            activityGroupPanel.Visible = false;
            trainPlacePanel.Visible = false;
        }

        private void preGenPanel_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void addGymWorkouts()
        {
            workouts.Clear();
            workoutsList.Controls.Clear();
            back.Clear();
            biceps.Clear();
            triceps.Clear();
            shoulders.Clear();
            chest.Clear();
            quadriceps.Clear();
            hamstrings.Clear();
            glutes.Clear();
            calves.Clear();
            core.Clear();
            int rowCount = 0;

            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string query = "SELECT * FROM WorkoutsListGym";
                using (SqlCommand comm = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chest.Add(reader["Chest"].ToString());
                            triceps.Add(reader["Triceps"].ToString());
                            shoulders.Add(reader["Shoulders"].ToString());
                            back.Add(reader["Back"].ToString());
                            biceps.Add(reader["Biceps"].ToString());
                            quadriceps.Add(reader["Quadriceps"].ToString());
                            hamstrings.Add(reader["Hamstrings"].ToString());
                            glutes.Add(reader["Glutes"].ToString());
                            calves.Add(reader["Calves"].ToString());
                            core.Add(reader["Core"].ToString());
                            rowCount++;
                        }
                    }
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                WorkoutListItem workout = new WorkoutListItem();
                workout.WorkoutNumber = $"Workout {i + 1}";
                workout.ButtonClicked += (sender, e) => WorkoutListItemGym_ButtonClicked(sender, e);
                workoutsList.Controls.Add(workout);
                workouts.Add(workout);
                if (back[i] != "None" && biceps[i] != "None" && chest[i] != "None" && triceps[i] != "None" && shoulders[i] != "None"
                    && quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] != "None" && core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n" +
                $"[Core]: Abs, Obliques...";
                }
                else if (back[i] == "None" && biceps[i] == "None" && chest[i] == "None" && triceps[i] == "None" && shoulders[i] == "None"
                    && quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] != "None" && core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n" +
                $"[Core]: Abs, Obliques...";
                }
                else if (back[i] == "None" && biceps[i] == "None" && chest[i] == "None" && triceps[i] == "None" && shoulders[i] == "None"
                    && quadriceps[i] == "None" && hamstrings[i] == "None" && glutes[i] == "None" && calves[i] == "None" && core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Core]: Abs, Obliques...";
                }
                else if (back[i] != "None" && biceps[i] != "None" && chest[i] != "None" && triceps[i] != "None" && shoulders[i] != "None"
                    && quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] != "None" && core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n";
                }
                else if (back[i] != "None" && biceps[i] != "None" && chest[i] != "None" && triceps[i] != "None" && shoulders[i] != "None"
                   && quadriceps[i] == "None" && hamstrings[i] == "None" && glutes[i] == "None" && calves[i] == "None" && core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n";
                }
                else if (back[i] != "None" && biceps[i] != "None" && chest[i] != "None" && triceps[i] != "None" && shoulders[i] != "None"
                  && quadriceps[i] == "None" && hamstrings[i] == "None" && glutes[i] == "None" && calves[i] == "None" && core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                $"[Core]: Abs, Obliques...";
                }
                else if (back[i] == "None" && biceps[i] == "None" && chest[i] == "None" && triceps[i] == "None" && shoulders[i] == "None"
                  && quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] != "None" && core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n";
                }
            }
        }
        private void addHomeWorkouts()
        {
            workouts.Clear();
            workoutsList.Controls.Clear();

            backbiceps.Clear();
            chesttricepsshoulders.Clear();
            legs.Clear();
            _core.Clear();
            int rowCount = 0;

            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string query = "SELECT * FROM WorkoutsListHome";
                using (SqlCommand comm = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chesttricepsshoulders.Add(reader["ChestTricepsShoulders"].ToString());
                            backbiceps.Add(reader["BackBiceps"].ToString());
                            legs.Add(reader["Legs"].ToString());
                            _core.Add(reader["Core"].ToString());
                            rowCount++;
                        }
                    }
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                WorkoutListItem workout = new WorkoutListItem();
                workout.WorkoutNumber = $"Workout {i + 1}";
                workout.ButtonClicked += (sender, e) => HomeOutdoors_ButtonClicked(sender, e);
                workoutsList.Controls.Add(workout);
                workouts.Add(workout);
                if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] != "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n" +
                $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] == "None" && chesttricepsshoulders[i] == "None" && legs[i] != "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n" +
                        $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] == "None" && chesttricepsshoulders[i] == "None" && legs[i] == "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] == "None" && _core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n";
                }
                else if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] != "None" && _core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                        $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...";
                }
                else if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] == "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                        $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] == "None" && chesttricepsshoulders[i] == "None" && legs[i] != "None" && _core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...";
                }
            }
        }
        private void addOutdoorsWorkouts()
        {
            workouts.Clear();
            workoutsList.Controls.Clear();
            backbiceps.Clear();
            chesttricepsshoulders.Clear();
            legs.Clear();
            _core.Clear();
            int rowCount = 0;

            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string query = "SELECT * FROM WorkoutsListOutdoors";
                using (SqlCommand comm = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chesttricepsshoulders.Add(reader["ChestTricepsShoulders"].ToString());
                            backbiceps.Add(reader["BackBiceps"].ToString());
                            legs.Add(reader["Legs"].ToString());
                            _core.Add(reader["Core"].ToString());
                            rowCount++;
                        }
                    }
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                WorkoutListItem workout = new WorkoutListItem();
                workout.WorkoutNumber = $"Workout {i + 1}";
                workout.ButtonClicked += (sender, e) => HomeOutdoors_ButtonClicked(sender, e);
                workoutsList.Controls.Add(workout);
                workouts.Add(workout);
                if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] != "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n" +
                $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] == "None" && chesttricepsshoulders[i] == "None" && legs[i] != "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...\n" +
                        $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] == "None" && chesttricepsshoulders[i] == "None" && legs[i] == "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] == "None" && _core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n";
                }
                else if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] != "None" && _core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                        $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...";
                }
                else if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None" && legs[i] == "None" && _core[i] != "None")
                {
                    workouts[i].WorkoutExercises = $"[Upper]: Back, Biceps, Chest, Triceps, Shoulders...\n" +
                        $"[Core]: Abs, Obliques...";
                }
                else if (backbiceps[i] == "None" && chesttricepsshoulders[i] == "None" && legs[i] != "None" && _core[i] == "None")
                {
                    workouts[i].WorkoutExercises = $"[Lower]: Quadriceps, Hamstrings, Glutes, Calves...";
                }
            }
        }
        private void workoutsList_VisibleChanged(object sender, EventArgs e)
        {
            if (gymClicked)
            {
                addGymWorkouts();
            }
            else if (homeClicked)
            {
                addHomeWorkouts();
            }
            else if (outdoorsClicked)
            {
                addOutdoorsWorkouts();
            }
        }

        private void workoutListPanel_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void calvesPicture_Click(object sender, EventArgs e)
        {
            if (calvesCheckBox.Checked)
            {
                calvesPicture.Image = Properties.Resources.calves;
                calvesCheckBox.Checked = false;
            }
            else
            {
                calvesCheckBox.Checked = true;
                calvesPicture.Image = Properties.Resources.calvesSelected;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void glutesPicture_Click(object sender, EventArgs e)
        {
            if (glutesCheckBox.Checked)
            {
                glutesPicture.Image = Properties.Resources.glutes;
                glutesCheckBox.Checked = false;
            }
            else
            {
                glutesCheckBox.Checked = true;
                glutesPicture.Image = Properties.Resources.glutesSelected;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void hamstringsPicture_Click(object sender, EventArgs e)
        {
            if (hamstringsCheckBox.Checked)
            {
                hamstringsPicture.Image = Properties.Resources.hamstrings;
                hamstringsCheckBox.Checked = false;
            }
            else
            {
                hamstringsCheckBox.Checked = true;
                hamstringsPicture.Image = Properties.Resources.hamstringsSelected;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void quadricepsPicture_Click(object sender, EventArgs e)
        {
            if (quadricepsCheckBox.Checked)
            {
                quadricepsPicture.Image = Properties.Resources.quadriceps;
                quadricepsCheckBox.Checked = false;
            }
            else
            {
                quadricepsCheckBox.Checked = true;
                quadricepsPicture.Image = Properties.Resources.quadricepsSelected;
            }
            if (!hamstringsCheckBox.Checked && !quadricepsCheckBox.Checked && !glutesCheckBox.Checked && !calvesCheckBox.Checked)
            {
                nextButton5.Visible = false;
            }
            else nextButton5.Visible = true;
        }

        private void backPicture_Click(object sender, EventArgs e)
        {
            if (backCheckBox.Checked)
            {
                backPicture.Image = Properties.Resources.backSelected;
                backCheckBox.Checked = false;
            }
            else
            {
                backPicture.Image = Properties.Resources.back;
                backCheckBox.Checked = true;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void armsPicture_Click(object sender, EventArgs e)
        {
            if (armsCheckBox.Checked)
            {
                armsPicture.Image = Properties.Resources.armsSelected;
                armsCheckBox.Checked = false;
            }
            else
            {
                armsPicture.Image = Properties.Resources.arms;
                armsCheckBox.Checked = true;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void shouldersPicture_Click(object sender, EventArgs e)
        {
            if (shouldersCheckBox.Checked)
            {
                shouldersPicture.Image = Properties.Resources.shouldersSelected;
                shouldersCheckBox.Checked = false;
            }
            else
            {
                shouldersPicture.Image = Properties.Resources.shoulders;
                shouldersCheckBox.Checked = true;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void chestPicture_Click(object sender, EventArgs e)
        {
            if (chestCheckBox.Checked)
            {
                chestPicture.Image = Properties.Resources.chestSelected;
                chestCheckBox.Checked = false;
            }
            else
            {
                chestPicture.Image = Properties.Resources.chest;
                chestCheckBox.Checked = true;
            }
            if (!armsCheckBox.Checked && !shouldersCheckBox.Checked && !chestCheckBox.Checked && !backCheckBox.Checked)
            {
                nextButton4.Visible = false;
            }
            else nextButton4.Visible = true;
        }

        private void calorieButtonClose_Click(object sender, EventArgs e)
        {
            workoutPreviewPanel.Visible = false;
        }

        private void calorieButtonClose_MouseEnter(object sender, EventArgs e)
        {
            workoutPreviewClose.BackColor = Color.IndianRed;
        }

        private void calorieButtonClose_MouseLeave(object sender, EventArgs e)
        {
            workoutPreviewClose.BackColor = Color.WhiteSmoke;
        }
        private void WorkoutListItemGym_ButtonClicked(object sender, EventArgs e)
        {
            WorkoutDays.Clear();
            BackWorkout = "";
            BicepsWorkout = "";
            ChestWorkout = "";
            TricepsWorkout = "";
            ShouldersWorkout = "";
            QuadricepsWorkout = "";
            HamstringsWorkout = "";
            GlutesWorkout = "";
            CalvesWorkout = "";
            CoreWorkout = "";
            for (int i = 0; i < workouts.Count; i++)
            {
                if (workouts[i].Equals(sender))
                {
                    workoutPreviewPanel.Visible = true;
                    workoutPreviewPanel.BringToFront();
                    workoutPreviewLabel.Text = $"Workout {i + 1}";
                    labelExercises.Text = $"[Back]: {back[i]}\n[Biceps]: {biceps[i]}\n[Chest]: {chest[i]}\n[Triceps]: {triceps[i]}\n[Shoulders]: {shoulders[i]}\n\n" +
                    $"[Quadriceps]: {quadriceps[i]}\n[Hamstrings]: {hamstrings[i]}\n[Glutes]: {glutes[i]}\n[Calves]: {calves[i]}\n\n" +
                    $"[Core]: {core[i]}";

                    if (back[i] != "None" && biceps[i] != "None")
                    {
                        WorkoutDays.Add("Back and Biceps");
                        BackWorkout = back[i];
                        BicepsWorkout = biceps[i];
                    }
                    else if (back[i] != "None" && biceps[i] == "None")
                    {
                        WorkoutDays.Add("Back");
                        BackWorkout = back[i];
                    }
                    else if (back[i] == "None" && biceps[i] != "None")
                    {
                        WorkoutDays.Add("Biceps");
                        BicepsWorkout = biceps[i];
                    }
                    if (chest[i] != "None" && triceps[i] != "None" && shoulders[i] != "None")
                    {
                        WorkoutDays.Add("Chest, Triceps and Shoulders");
                        ChestWorkout = chest[i];
                        TricepsWorkout = triceps[i];
                        ShouldersWorkout = shoulders[i];
                    }
                    else if (chest[i] != "None" && triceps[i] == "None" && shoulders[i] == "None")
                    {
                        WorkoutDays.Add("Chest");
                        ChestWorkout = chest[i];
                    }
                    else if (chest[i] == "None" && triceps[i] != "None" && shoulders[i] == "None")
                    {
                        WorkoutDays.Add("Triceps");
                        TricepsWorkout = triceps[i];
                    }
                    else if (chest[i] == "None" && triceps[i] == "None" && shoulders[i] != "None")
                    {
                        WorkoutDays.Add("Shoulders");
                        ShouldersWorkout = shoulders[i];
                    }
                    else if (chest[i] != "None" && triceps[i] != "None" && shoulders[i] == "None")
                    {
                        WorkoutDays.Add("Chest and Triceps");
                        ChestWorkout = chest[i];
                        TricepsWorkout = triceps[i];
                    }
                    else if (chest[i] != "None" && triceps[i] == "None" && shoulders[i] != "None")
                    {
                        WorkoutDays.Add("Chest and Shoulders");
                        ChestWorkout = chest[i];
                        ShouldersWorkout = shoulders[i];
                    }
                    else if (chest[i] == "None" && triceps[i] != "None" && shoulders[i] != "None")
                    {
                        WorkoutDays.Add("Triceps and Shoulders");
                        TricepsWorkout = triceps[i];
                        ShouldersWorkout = shoulders[i];
                    }
                    if (quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Legs");
                        QuadricepsWorkout = quadriceps[i];
                        HamstringsWorkout = hamstrings[i];
                        GlutesWorkout = glutes[i];
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] == "None" && glutes[i] == "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Quadriceps");
                        QuadricepsWorkout = quadriceps[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] != "None" && glutes[i] == "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Hamstrings");
                        HamstringsWorkout = hamstrings[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] == "None" && glutes[i] != "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Glutes");
                        GlutesWorkout = glutes[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] == "None" && glutes[i] == "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Calves");
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] == "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Quadriceps and Hamstrings");
                        QuadricepsWorkout = quadriceps[i];
                        HamstringsWorkout = hamstrings[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] == "None" && glutes[i] != "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Quadriceps and Glutes");
                        QuadricepsWorkout = quadriceps[i];
                        GlutesWorkout = glutes[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] == "None" && glutes[i] == "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Quadriceps and Calves");
                        QuadricepsWorkout = quadriceps[i];
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Hamstrings and Glutes");
                        HamstringsWorkout = hamstrings[i];
                        GlutesWorkout = glutes[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] != "None" && glutes[i] == "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Hamstrings and Calves");
                        HamstringsWorkout = hamstrings[i];
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] == "None" && glutes[i] != "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Glutes and Calves");
                        GlutesWorkout = glutes[i];
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] == "None")
                    {
                        WorkoutDays.Add("Quadriceps, Hamstrings and Glutes");
                        QuadricepsWorkout = quadriceps[i];
                        HamstringsWorkout = hamstrings[i];
                        GlutesWorkout = glutes[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] != "None" && glutes[i] == "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Quadriceps, Hamstrings and Calves");
                        QuadricepsWorkout = quadriceps[i];
                        HamstringsWorkout = hamstrings[i];
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] != "None" && hamstrings[i] == "None" && glutes[i] != "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Quadriceps, Glutes and Calves");
                        QuadricepsWorkout = quadriceps[i];
                        GlutesWorkout = glutes[i];
                        CalvesWorkout = calves[i];
                    }
                    else if (quadriceps[i] == "None" && hamstrings[i] != "None" && glutes[i] != "None" && calves[i] != "None")
                    {
                        WorkoutDays.Add("Hamstrings, Glutes and Calves");
                        HamstringsWorkout = hamstrings[i];
                        GlutesWorkout = glutes[i];
                        CalvesWorkout = calves[i];
                    }
                    if (core[i] != "None")
                    {
                        WorkoutDays.Add("Core");
                        CoreWorkout = core[i];
                    }
                }
            }
        }
        private void HomeOutdoors_ButtonClicked(object sender, EventArgs e)
        {
            WorkoutDays.Clear();
            BackBicepsWorkout = "";
            ChestTricepsShouldersWorkout = "";
            LegsWorkout = "";
            CoreWorkout = "";
            for (int i = 0; i < workouts.Count; i++)
            {
                if (workouts[i].Equals(sender))
                {
                    workoutPreviewPanel.Visible = true;
                    workoutPreviewPanel.BringToFront();
                    workoutPreviewLabel.Text = $"Workout {i + 1}";
                    labelExercises.Text = $"[Back and Biceps]: {backbiceps[i]}\n[Chest, Triceps and Shoulders]: {chesttricepsshoulders[i]}\n\n" +
                    $"[Legs]: {legs[i]}\n\n" +
                    $"[Core]: {_core[i]}";

                    if (backbiceps[i] != "None" && chesttricepsshoulders[i] != "None")
                    {
                        WorkoutDays.Add("Back and Biceps");
                        WorkoutDays.Add("Chest, Triceps and Shoulders");
                        BackBicepsWorkout = backbiceps[i];
                        ChestTricepsShouldersWorkout = chesttricepsshoulders[i];
                    }
                    else if (backbiceps[i] != "None" && chesttricepsshoulders[i] == "None")
                    {
                        WorkoutDays.Add("Back and Biceps");
                        BackBicepsWorkout = backbiceps[i];
                    }
                    else if (backbiceps[i] == "None" && chesttricepsshoulders[i] != "None")
                    {
                        WorkoutDays.Add("Chest, Triceps and Shoulders");
                        ChestTricepsShouldersWorkout = chesttricepsshoulders[i];
                    }
                    if (legs[i] != "None")
                    {
                        WorkoutDays.Add("Legs");
                        LegsWorkout = legs[i];
                    }
                    if (_core[i] != "None")
                    {
                        WorkoutDays.Add("Core");
                        CoreWorkout = _core[i];
                    }
                }
            }
        }
        List<string> WorkoutDays = new List<string>();

        string BackWorkout = "";
        string BicepsWorkout = "";
        string ChestWorkout = "";
        string TricepsWorkout = "";
        string ShouldersWorkout = "";
        string QuadricepsWorkout = "";
        string HamstringsWorkout = "";
        string GlutesWorkout = "";
        string CalvesWorkout = "";

        string BackBicepsWorkout = "";
        string ChestTricepsShouldersWorkout = "";
        string LegsWorkout = "";

        string CoreWorkout = "";
        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            trainingDaysPanel.Visible = true; trainingDaysPanel.BringToFront();
            workoutPreviewPanel.Visible = false;
            workoutsList.Visible = false;
            workoutsLabel.Visible = false;
        }

        private void preNextButton_Click(object sender, EventArgs e)
        {

        }
        private void weekDayClick(CheckBox a, PictureBox b, Label c)
        {
            if (a.Checked && selectedDays <= activity)
            {
                selectedDays++;
                b.Image = Properties.Resources.weekBordersChecked;
                c.BackColor = Color.FromArgb(102, 249, 108);
                a.BackColor = Color.FromArgb(102, 249, 108);
            }
            else if (!a.Checked)
            {
                selectedDays--;
                b.Image = Properties.Resources.weekBorders;
                c.BackColor = Color.WhiteSmoke;
                a.BackColor = Color.WhiteSmoke;
            }
            if (selectedDays == activity + 1)
            {
                a.Checked = false;
            }
            if (selectedDays == activity && selectedDays != 0)
            {
                preNextButton2.Visible = true;
            }
            else
            {
                preNextButton2.Visible = false;
            }
        }
        private void mondayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(mondayCheckBox, mondayPictureBox, mondayLabel);
            if (mondayCheckBox.Checked)
            {
                mondayClicked = true;
            }
            else { mondayClicked = false; }
        }

        private void mondayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void tuesdayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void wednesdayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void thursdayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void fridayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void saturdayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void sundayPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void tuesdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(tuesdayCheckBox, tuesdayPictureBox, tuesdayLabel);
            if (tuesdayCheckBox.Checked)
            {
                tuesdayClicked = true;
            }
            else { tuesdayClicked = false; }
        }

        private void wednesdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(wednesdayCheckBox, wednesdayPictureBox, wednesdayLabel);
            if (wednesdayCheckBox.Checked)
            {
                wednesdayClicked = true;
            }
            else { wednesdayClicked = false; }
        }

        private void thursdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(thursdayCheckBox, thursdayPictureBox, thursdayLabel);
            if (thursdayCheckBox.Checked)
            {
                thursdayClicked = true;
            }
            else { thursdayClicked = false; }
        }

        private void fridayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(fridayCheckBox, fridayPictureBox, fridayLabel);
            if (fridayCheckBox.Checked)
            {
                fridayClicked = true;
            }
            else { fridayClicked = false; }
        }

        private void saturdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(saturdayCheckBox, saturdayPictureBox, saturdayLabel);
            if (saturdayCheckBox.Checked)
            {
                saturdayClicked = true;
            }
            else { saturdayClicked = false; }
        }

        private void sundayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            weekDayClick(sundayCheckBox, sundayPictureBox, sundayLabel);
            if (sundayCheckBox.Checked)
            {
                sundayClicked = true;
            }
            else { sundayClicked = false; }
        }

        private void preNextButton2_Click(object sender, EventArgs e)
        {
            trainingDaysPanel.Visible = false;
            selectWorkoutsPanel.Visible = true;
            if (mondayClicked)
            {
                monComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    monComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay1.Visible = true; }
            if (tuesdayClicked)
            {
                tueComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    tueComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay2.Visible = true; }
            if (wednesdayClicked)
            {
                wedComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    wedComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay3.Visible = true; }
            if (thursdayClicked)
            {
                thuComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    thuComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay4.Visible = true; }
            if (fridayClicked)
            {
                friComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    friComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay5.Visible = true; }
            if (saturdayClicked)
            {
                satComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    satComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay6.Visible = true; }
            if (sundayClicked)
            {
                sunComboBox.Visible = true;
                for (int i = 0; i < WorkoutDays.Count; i++)
                {
                    sunComboBox.Items.Add(WorkoutDays[i]);
                }
            }
            else { restDay7.Visible = true; }
        }
        private void WorkoutDaysComboBox_Click(object sender, EventArgs e)
        {
            if (monComboBox.Visible == true)
            {
                if (monComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (tueComboBox.Visible == true)
            {
                if (tueComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (wedComboBox.Visible == true)
            {
                if (wedComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (thuComboBox.Visible == true)
            {
                if (thuComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (friComboBox.Visible == true)
            {
                if (friComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (satComboBox.Visible == true)
            {
                if (satComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (sunComboBox.Visible == true)
            {
                if (sunComboBox.Text != "")
                {
                    workoutDaysDone = true;
                }
                else workoutDaysDone = false;
            }
            if (workoutDaysDone)
            {
                preNextButton3.Visible = true;
            }
            else preNextButton3.Visible = false;
        }

        private void preNextButton3_Click(object sender, EventArgs e)
        {
            selectWorkoutsPanel.Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";
                query = "UPDATE Workouts " +
                        "SET Monday = @Monday, Tuesday = @Tuesday, Wednesday = @Wednesday, Thursday = @Thursday, Friday = @Friday, Saturday = @Saturday, Sunday = @Sunday " +
                        "WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", _userID);
                    if (mondayClicked)
                    {
                        saveWorkoutParameter(monComboBox, "@Monday", command);
                    }
                    else command.Parameters.AddWithValue("@Monday", "Rest Day");
                    if (tuesdayClicked)
                    {
                        saveWorkoutParameter(tueComboBox, "@Tuesday", command);
                    }
                    else command.Parameters.AddWithValue("@Tuesday", "Rest Day");
                    if (wednesdayClicked)
                    {
                        saveWorkoutParameter(wedComboBox, "@Wednesday", command);
                    }
                    else command.Parameters.AddWithValue("@Wednesday", "Rest Day");
                    if (thursdayClicked)
                    {
                        saveWorkoutParameter(thuComboBox, "@Thursday", command);
                    }
                    else command.Parameters.AddWithValue("@Thursday", "Rest Day");
                    if (fridayClicked)
                    {
                        saveWorkoutParameter(friComboBox, "@Friday", command);
                    }
                    else command.Parameters.AddWithValue("@Friday", "Rest Day");
                    if (saturdayClicked)
                    {
                        saveWorkoutParameter(satComboBox, "@Saturday", command);
                    }
                    else command.Parameters.AddWithValue("@Saturday", "Rest Day");
                    if (sundayClicked)
                    {
                        saveWorkoutParameter(sunComboBox, "@Sunday", command);
                    }
                    else command.Parameters.AddWithValue("@Sunday", "Rest Day");
                    command.ExecuteNonQuery();
                }
            }
            workoutsDashboard.Visible = true;
            workoutsDashboard.BringToFront();
            trainPlacePanel.Visible = false;
        }
        private void saveWorkoutParameter(KryptonComboBox cb, string day, SqlCommand command)
        {
            if (preClicked)
            {
                if (gymClicked)
                {
                    if (cb.Text == "Back and Biceps")
                    {
                        command.Parameters.AddWithValue(day, BackWorkout + BicepsWorkout);
                    }
                    else if (cb.Text == "Chest")
                    {
                        command.Parameters.AddWithValue(day, ChestWorkout);
                    }
                    else if (cb.Text == "Triceps")
                    {
                        command.Parameters.AddWithValue(day, TricepsWorkout);
                    }
                    else if (cb.Text == "Shoulders")
                    {
                        command.Parameters.AddWithValue(day, ShouldersWorkout);
                    }
                    else if (cb.Text == "Chest and Triceps")
                    {
                        command.Parameters.AddWithValue(day, ChestWorkout + TricepsWorkout);
                    }
                    else if (cb.Text == "Chest and Shoulders")
                    {
                        command.Parameters.AddWithValue(day, ChestWorkout + ShouldersWorkout);
                    }
                    else if (cb.Text == "Triceps and Shoulders")
                    {
                        command.Parameters.AddWithValue(day, TricepsWorkout + ShouldersWorkout);
                    }
                    else if (cb.Text == "Quadriceps")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout);
                    }
                    else if (cb.Text == "Hamstrings")
                    {
                        command.Parameters.AddWithValue(day, HamstringsWorkout);
                    }
                    else if (cb.Text == "Glutes")
                    {
                        command.Parameters.AddWithValue(day, GlutesWorkout);
                    }
                    else if (cb.Text == "Calves")
                    {
                        command.Parameters.AddWithValue(day, CalvesWorkout);
                    }
                    else if (cb.Text == "Quadriceps and Hamstrings")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + HamstringsWorkout);
                    }
                    else if (cb.Text == "Quadriceps and Glutes")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + GlutesWorkout);
                    }
                    else if (cb.Text == "Quadriceps and Calves")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + CalvesWorkout);
                    }
                    else if (cb.Text == "Hamstrings and Glutes")
                    {
                        command.Parameters.AddWithValue(day, HamstringsWorkout + GlutesWorkout);
                    }
                    else if (cb.Text == "Glutes and Calves")
                    {
                        command.Parameters.AddWithValue(day, GlutesWorkout + CalvesWorkout);
                    }
                    else if (cb.Text == "Quadriceps, Hamstrings and Glutes")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + HamstringsWorkout + GlutesWorkout);
                    }
                    else if (cb.Text == "Quadriceps, Hamstrings and Calves")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + HamstringsWorkout + CalvesWorkout);
                    }
                    else if (cb.Text == "Quadriceps, Glutes and Calves")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + GlutesWorkout + CalvesWorkout);
                    }
                    else if (cb.Text == "Hamstrings, Glutes and Calves")
                    {
                        command.Parameters.AddWithValue(day, HamstringsWorkout + GlutesWorkout + CalvesWorkout);
                    }
                    else if (cb.Text == "Legs")
                    {
                        command.Parameters.AddWithValue(day, QuadricepsWorkout + HamstringsWorkout + GlutesWorkout + CalvesWorkout);
                    }
                    else if (cb.Text == "Core")
                    {
                        command.Parameters.AddWithValue(day, CoreWorkout);
                    }
                    else if (cb.Text == "Chest, Triceps and Shoulders")
                    {
                        command.Parameters.AddWithValue(day, ChestWorkout + TricepsWorkout + ShouldersWorkout);
                    }
                }
                else
                {
                    if (cb.Text == "Back and Biceps")
                    {
                        command.Parameters.AddWithValue(day, BackBicepsWorkout);
                    }
                    else if (cb.Text == "Chest, Triceps and Shoulders")
                    {
                        command.Parameters.AddWithValue(day, ChestTricepsShouldersWorkout);
                    }
                    else if (cb.Text == "Legs")
                    {
                        command.Parameters.AddWithValue(day, LegsWorkout);
                    }
                    else if (cb.Text == "Core")
                    {
                        command.Parameters.AddWithValue(day, CoreWorkout);
                    }
                }
            }
            else
            {
                if (cb.Text == "Workout 1")
                {
                    command.Parameters.AddWithValue(day, workout1);
                }
                else if (cb.Text == "Workout 2")
                {
                    command.Parameters.AddWithValue(day, workout2);
                }
                else if (cb.Text == "Workout 3")
                {
                    command.Parameters.AddWithValue(day, workout3);
                }
                else if (cb.Text == "Workout 4")
                {
                    command.Parameters.AddWithValue(day, workout4);
                }
                else if (cb.Text == "Workout 5")
                {
                    command.Parameters.AddWithValue(day, workout5);
                }
                else if (cb.Text == "Workout 6")
                {
                    command.Parameters.AddWithValue(day, workout6);
                }
                else if (cb.Text == "Workout 7")
                {
                    command.Parameters.AddWithValue(day, workout7);
                }
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            workoutsDashboard.Visible = false;
            trainPlacePanel.Visible = true;
        }

        private void editWorkoutLabel_Click(object sender, EventArgs e)
        {
        }

        private void editWorkoutLabel_MouseEnter(object sender, EventArgs e)
        {
            editWorkoutButton.FillColor = Color.FromArgb(211, 211, 211);
            editWorkoutLabel.BackColor = Color.FromArgb(211, 211, 211);
        }

        private void editWorkoutLabel_MouseLeave(object sender, EventArgs e)
        {
            editWorkoutButton.FillColor = Color.Transparent;
            editWorkoutLabel.BackColor = Color.Transparent;
        }

        private void workoutsDashboard_VisibleChanged(object sender, EventArgs e)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                date1.Text = DateTime.Now.ToString("dd.MM");
                date1.ForeColor = Color.Green;
                wdMonday.ForeColor = Color.Green;
                date2.Text = DateTime.Now.AddDays(1).ToString("dd.MM");
                date3.Text = DateTime.Now.AddDays(2).ToString("dd.MM");
                date4.Text = DateTime.Now.AddDays(3).ToString("dd.MM");
                date5.Text = DateTime.Now.AddDays(4).ToString("dd.MM");
                date6.Text = DateTime.Now.AddDays(5).ToString("dd.MM");
                date7.Text = DateTime.Now.AddDays(6).ToString("dd.MM");
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                date1.Text = DateTime.Now.AddDays(-1).ToString("dd.MM");
                date2.Text = DateTime.Now.ToString("dd.MM");
                date2.ForeColor = Color.Green;
                wdTuesday.ForeColor = Color.Green;
                date3.Text = DateTime.Now.AddDays(1).ToString("dd.MM");
                date4.Text = DateTime.Now.AddDays(2).ToString("dd.MM");
                date5.Text = DateTime.Now.AddDays(3).ToString("dd.MM");
                date6.Text = DateTime.Now.AddDays(4).ToString("dd.MM");
                date7.Text = DateTime.Now.AddDays(5).ToString("dd.MM");
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                date1.Text = DateTime.Now.AddDays(-2).ToString("dd.MM");
                date2.Text = DateTime.Now.AddDays(-1).ToString("dd.MM");
                date3.Text = DateTime.Now.ToString("dd.MM");
                date3.ForeColor = Color.Green;
                wdWednesday.ForeColor = Color.Green;
                date4.Text = DateTime.Now.AddDays(1).ToString("dd.MM");
                date5.Text = DateTime.Now.AddDays(2).ToString("dd.MM");
                date6.Text = DateTime.Now.AddDays(3).ToString("dd.MM");
                date7.Text = DateTime.Now.AddDays(4).ToString("dd.MM");
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                date1.Text = DateTime.Now.AddDays(-3).ToString("dd.MM");
                date2.Text = DateTime.Now.AddDays(-2).ToString("dd.MM");
                date3.Text = DateTime.Now.AddDays(-1).ToString("dd.MM");
                date4.Text = DateTime.Now.ToString("dd.MM");
                date4.ForeColor = Color.Green;
                wdThursday.ForeColor = Color.Green;
                date5.Text = DateTime.Now.AddDays(1).ToString("dd.MM");
                date6.Text = DateTime.Now.AddDays(2).ToString("dd.MM");
                date7.Text = DateTime.Now.AddDays(3).ToString("dd.MM");
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                date1.Text = DateTime.Now.AddDays(-4).ToString("dd.MM");
                date2.Text = DateTime.Now.AddDays(-3).ToString("dd.MM");
                date3.Text = DateTime.Now.AddDays(-2).ToString("dd.MM");
                date4.Text = DateTime.Now.AddDays(-1).ToString("dd.MM");
                date5.Text = DateTime.Now.ToString("dd.MM");
                date5.ForeColor = Color.Green;
                wdFriday.ForeColor = Color.Green;
                date6.Text = DateTime.Now.AddDays(1).ToString("dd.MM");
                date7.Text = DateTime.Now.AddDays(2).ToString("dd.MM");
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                date1.Text = DateTime.Now.AddDays(-5).ToString("dd.MM");
                date2.Text = DateTime.Now.AddDays(-4).ToString("dd.MM");
                date3.Text = DateTime.Now.AddDays(-3).ToString("dd.MM");
                date4.Text = DateTime.Now.AddDays(-2).ToString("dd.MM");
                date5.Text = DateTime.Now.AddDays(-1).ToString("dd.MM");
                date6.Text = DateTime.Now.ToString("dd.MM");
                date6.ForeColor = Color.Green;
                wdSaturday.ForeColor = Color.Green;
                date7.Text = DateTime.Now.AddDays(1).ToString("dd.MM");
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                date1.Text = DateTime.Now.AddDays(-6).ToString("dd.MM");
                date2.Text = DateTime.Now.AddDays(-5).ToString("dd.MM");
                date3.Text = DateTime.Now.AddDays(-4).ToString("dd.MM");
                date4.Text = DateTime.Now.AddDays(-3).ToString("dd.MM");
                date5.Text = DateTime.Now.AddDays(-2).ToString("dd.MM");
                date6.Text = DateTime.Now.AddDays(-1).ToString("dd.MM");
                date7.Text = DateTime.Now.ToString("dd.MM");
                date7.ForeColor = Color.Green;
                wdSunday.ForeColor = Color.Green;
            }

            if (workoutSelection == "")
            {
                wdSelectedDay.Text = "None";
                wdSelectedExercises.Text = "N/A";
            }
        }

        private void trainPlacePanel_VisibleChanged(object sender, EventArgs e)
        {
            if (trainPlacePanel.Visible == true)
            {
                gymClicked = false;
                homeClicked = false;
                outdoorsClicked = false;
                gymButton.Image = Properties.Resources.gym;
                homeButton.Image = Properties.Resources.home1;
                outdoorsButton.Image = Properties.Resources.outdoors;
                activityLevelComboBox.Text = "";
                activityLevelComboBox.SelectedValue = "";
                activityLevelComboBox.SelectedText = "";
                activityLevelComboBox.SelectedIndex = -1;
                activity = 0;
                nextButton1.Visible = false;
                nextButton2.Visible = false;
                nextButton3.Visible = false;
                nextButton4.Visible = false;
                nextButton5.Visible = false;
                nextButton7.Visible = false;
                nextButt7.Visible = false;
                preNextButton.Visible = false;
                preNextButton2.Visible = false;
                preNextButton3.Visible = false;
                preClicked = false;
                createClicked = false;
                prePicture.Image = Properties.Resources.pre;
                createPicture.Image = Properties.Resources.create;
                mondayCheckBox.Checked = false;
                tuesdayCheckBox.Checked = false;
                wednesdayCheckBox.Checked = false;
                thursdayCheckBox.Checked = false;
                fridayCheckBox.Checked = false;
                saturdayCheckBox.Checked = false;
                sundayCheckBox.Checked = false;
                mondayClicked = false;
                tuesdayClicked = false;
                wednesdayClicked = false;
                thursdayClicked = false;
                fridayClicked = false;
                saturdayClicked = false;
                sundayClicked = false;
                monComboBox.Items.Clear();
                tueComboBox.Items.Clear();
                wedComboBox.Items.Clear();
                thuComboBox.Items.Clear();
                friComboBox.Items.Clear();
                satComboBox.Items.Clear();
                sunComboBox.Items.Clear();
                restDay1.Visible = false;
                restDay2.Visible = false;
                restDay3.Visible = false;
                restDay4.Visible = false;
                restDay5.Visible = false;
                restDay6.Visible = false;
                restDay7.Visible = false;
                monComboBox.Visible = false;
                tueComboBox.Visible = false;
                wedComboBox.Visible = false;
                thuComboBox.Visible = false;
                friComboBox.Visible = false;
                satComboBox.Visible = false;
                sunComboBox.Visible = false;
                workoutDaysDone = false;
                selectedDays = 0;
                workoutNumber = 0;
                upperBodyClicked = false;
                upperButton.Image = Properties.Resources.upper;
                lowerBodyClicked = false;
                lowerButton.Image = Properties.Resources.lower;
                coreClicked = false;
                coreButton.Image = Properties.Resources.core;
                backCheckBox.Checked = false;
                backPicture.Image = Properties.Resources.back;
                armsCheckBox.Checked = false;
                armsPicture.Image = Properties.Resources.arms;
                chestCheckBox.Checked = false;
                chestPicture.Image = Properties.Resources.chest;
                shouldersCheckBox.Checked = false;
                shouldersPicture.Image = Properties.Resources.shoulders;
                quadricepsCheckBox.Checked = false;
                quadricepsPicture.Image = Properties.Resources.quadriceps;
                hamstringsCheckBox.Checked = false;
                hamstringsPicture.Image = Properties.Resources.hamstrings;
                glutesCheckBox.Checked = false;
                glutesPicture.Image = Properties.Resources.glutes;
                calvesCheckBox.Checked = false;
                calvesPicture.Image = Properties.Resources.calves;
                exerciseListItems.Clear();
                exerciseList.Clear();
                exerciseListPanel.Controls.Clear();
                currentExerciseList.Controls.Clear();
                workoutNumberComboBox.Text = "";
                workoutNumberComboBox.SelectedIndex = -1;
                chooseWorkoutNumberPanel.Visible = false;
                chestTricepsShoulderCheckBox.Checked = false;
                backBicepsCheckBox.Checked = false;
                frontCTS.Image = Properties.Resources.frontChestShouldersTriceps;
                backCTS.Image = Properties.Resources.backChestShouldersTriceps;
                frontBB.Image = Properties.Resources.frontBackBiceps;
                backBB.Image = Properties.Resources.backBackBiceps;
            }
        }
        private void workoutShow(string day)
        {
            wdSelectedExercises.Text = "";
            wdSelectedDay.Text = "";
            string monday_workout = "Rest Day";
            string tuesday_workout = "Rest Day";
            string wednesday_workout = "Rest Day";
            string thursday_workout = "Rest Day";
            string friday_workout = "Rest Day";
            string saturday_workout = "Rest Day";
            string sunday_workout = "Rest Day";
            List<string> monday_List = new List<string>();
            List<string> tuesday_List = new List<string>();
            List<string> wednesday_List = new List<string>();
            List<string> thursday_List = new List<string>();
            List<string> friday_List = new List<string>();
            List<string> saturday_List = new List<string>();
            List<string> sunday_List = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";
                query = "SELECT * FROM Workouts WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", _userID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["Monday"].ToString() != "Rest Day")
                            {
                                monday_List = reader["Monday"].ToString().Split(',').ToList();
                            }
                            if (reader["Tuesday"].ToString() != "Rest Day")
                            {
                                tuesday_List = reader["Tuesday"].ToString().Split(',').ToList();
                            }
                            if (reader["Wednesday"].ToString() != "Rest Day")
                            {
                                wednesday_List = reader["Wednesday"].ToString().Split(',').ToList();
                            }
                            if (reader["Thursday"].ToString() != "Rest Day")
                            {
                                thursday_List = reader["Thursday"].ToString().Split(',').ToList();
                            }
                            if (reader["Friday"].ToString() != "Rest Day")
                            {
                                friday_List = reader["Friday"].ToString().Split(',').ToList();
                            }
                            if (reader["Saturday"].ToString() != "Rest Day")
                            {
                                saturday_List = reader["Saturday"].ToString().Split(',').ToList();
                            }
                            if (reader["Sunday"].ToString() != "Rest Day")
                            {
                                sunday_List = reader["Sunday"].ToString().Split(',').ToList();
                            }
                        }
                    }
                }
            }
            if (day == "Monday")
            {
                wdSelectedDay.Text = "Monday";
                for (int i = 0; i < monday_List.Count; i++)
                {
                    if (i < monday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + monday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + monday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
            else if (day == "Tuesday")
            {
                wdSelectedDay.Text = "Tuesday";
                for (int i = 0; i < tuesday_List.Count; i++)
                {
                    if (i < tuesday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + tuesday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + tuesday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
            else if (day == "Wednesday")
            {
                wdSelectedDay.Text = "Wednesday";
                for (int i = 0; i < wednesday_List.Count; i++)
                {
                    if (i < wednesday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + wednesday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + wednesday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
            else if (day == "Thursday")
            {
                wdSelectedDay.Text = "Thursday";
                for (int i = 0; i < thursday_List.Count; i++)
                {
                    if (i < thursday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + thursday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + thursday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
            else if (day == "Friday")
            {
                wdSelectedDay.Text = "Friday";
                for (int i = 0; i < friday_List.Count; i++)
                {
                    if (i < friday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + friday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + friday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
            else if (day == "Saturday")
            {
                wdSelectedDay.Text = "Saturday";
                for (int i = 0; i < saturday_List.Count; i++)
                {
                    if (i < saturday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + saturday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + saturday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
            else if (day == "Sunday")
            {
                wdSelectedDay.Text = "Sunday";
                for (int i = 0; i < sunday_List.Count; i++)
                {
                    if (i < sunday_List.Count - 1) wdSelectedExercises.Text += "3x12 - " + sunday_List[i] + "\n";
                    else wdSelectedExercises.Text += "3x12 - " + sunday_List[i];
                }
                if (wdSelectedExercises.Text == "")
                {
                    wdSelectedExercises.Text = "None - Rest Day";
                }
            }
        }

        private void mondayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Monday");
        }

        private void tuesdayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Tuesday");
        }

        private void wednesdayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Wednesday");
        }

        private void thursdayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Thursday");
        }

        private void fridayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Friday");
        }

        private void saturdayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Saturday");
        }

        private void sundayShowButton_Click(object sender, EventArgs e)
        {
            workoutShow("Sunday");
        }

        private void chooseWorkoutNumberNextBut_Click(object sender, EventArgs e)
        {
            chooseWorkoutNumberPanel.Visible = false;
            createPanel.Visible = true;
            workoutsLabel.Visible = false;
            workoutNumber = int.Parse(workoutNumberComboBox.Text);
        }

        private void workoutNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (workoutNumberComboBox.Text != "")
            {
                chooseWorkoutNumberNextBut.Visible = true;
            }
        }

        List<ExerciseListItem> exerciseListItems = new List<ExerciseListItem>();
        List<Exercise> exerciseList = new List<Exercise>();
        string workout1 = "";
        string workout2 = "";
        string workout3 = "";
        string workout4 = "";
        string workout5 = "";
        string workout6 = "";
        string workout7 = "";
        private void createPanel_VisibleChanged(object sender, EventArgs e)
        {
            exerciseListItems.Clear();
            exerciseList.Clear();
            exerciseListPanel.Controls.Clear();
            currentExerciseList.Controls.Clear();
            createLabel.Text = "Workout creation - 1";
            loadItemsList();
        }
        private void loadItems(List<string> exercises, string type)
        {
            for (int i = 0; i < exercises.Count; i++)
            {
                ExerciseListItem exerciselistitem = new ExerciseListItem();
                exerciselistitem.ExerciseListName = exercises[i] + $" ({type})";
                exerciselistitem.ExerciseName = exercises[i];
                exerciselistitem.ButtonClicked += (sender, e) => addWorkoutItem(sender, e);
                exerciseListPanel.Controls.Add(exerciselistitem);
                exerciseListItems.Add(exerciselistitem);
            }
        }
        private void loadItemsList()
        {
            if (gymClicked)
            {
                if (armsCheckBox.Checked)
                {
                    loadItems(gymBiceps, "Biceps");
                    loadItems(gymBack, "Back");
                }
                if (chestCheckBox.Checked)
                {
                    loadItems(gymChest, "Chest");
                }
                if (shouldersCheckBox.Checked)
                {
                    loadItems(gymShoulders, "Shoulders");
                }
                if (quadricepsCheckBox.Checked)
                {
                    loadItems(gymQuadriceps, "Quadriceps");
                }
                if (hamstringsCheckBox.Checked)
                {
                    loadItems(gymHamstrings, "Hamstrings");
                }
                if (glutesCheckBox.Checked)
                {
                    loadItems(gymGlutes, "Glutes");
                }
                if (calvesCheckBox.Checked)
                {
                    loadItems(gymCalves, "Calves");
                }
                if (coreClicked)
                {
                    loadItems(gymCore, "Core");
                }
            }
            else if (homeClicked)
            {

            }
            else if (outdoorsClicked)
            {

            }
        }

        private void addWorkoutItem(object sender, EventArgs e)
        {
            for (int i = 0; i < exerciseListItems.Count; i++)
            {
                if (exerciseListItems[i].Equals(sender))
                {
                    exerciseListPanel.Controls.RemoveAt(i);
                    exerciseListItems.RemoveAt(i);
                    if (exerciseListItems.Count > 0)
                    {
                        Exercise exercise = new Exercise();
                        exercise.ExerciseName = exerciseListItems[i].ExerciseName;
                        exercise.ExerciseListName = exerciseListItems[i].ExerciseListName;
                        exercise.ButtonClicked += (sender, e) => removeWorkoutItem(sender, e);
                        currentExerciseList.Controls.Add(exercise);
                        exerciseList.Add(exercise);
                        createNextButton.Visible = true;
                    }
                    else createNextButton.Visible = false;
                }
            }
        }
        private void removeWorkoutItem(object sender, EventArgs e)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                if (exerciseList[i].Equals(sender))
                {
                    currentExerciseList.Controls.RemoveAt(i);
                    exerciseList.RemoveAt(i);
                    ExerciseListItem exerciselistitem = new ExerciseListItem();
                    if (exerciseList.Count > 0)
                    {
                        exerciselistitem.ExerciseListName = exerciseList[i].ExerciseListName;
                        exerciselistitem.ExerciseName = exerciseList[i].ExerciseName;
                        exerciselistitem.ButtonClicked += (sender, e) => addWorkoutItem(sender, e);
                        exerciseListPanel.Controls.Add(exerciselistitem);
                        exerciseListItems.Add(exerciselistitem);
                    }
                    else createNextButton.Visible = false;
                }
            }
        }
        int currentWorkoutNumber = 1;

        private void createWorkout(ref string workout, int workout_number, int titleLabel_Number)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                if (i! < exerciseList.Count - 1)
                {
                    workout += exerciseList[i].ExerciseName + ",";
                }
                else workout += exerciseList[i].ExerciseName;
            }
            WorkoutDays.Add($"Workout {workout_number}");
            createLabel.Text = $"Workout creation - {titleLabel_Number}";
            exerciseListItems.Clear();
            exerciseList.Clear();
            exerciseListPanel.Controls.Clear();
            currentExerciseList.Controls.Clear();
            loadItemsList();
            if (currentWorkoutNumber <= workoutNumber) currentWorkoutNumber++;
            else if (currentWorkoutNumber == workoutNumber - 1) createNextButton.Text = "Done";
            else
            {
                createPanel.Visible = false;
                trainingDaysPanel.Visible = true;
            }
        }
        private void createNextButton_Click(object sender, EventArgs e)
        {
            switch (currentWorkoutNumber)
            {
                case 1:
                    createWorkout(ref workout1, 1, 2);
                    break;
                case 2:
                    createWorkout(ref workout2, 2, 3);
                    break;
                case 3:
                    createWorkout(ref workout3, 3, 4);
                    break;
                case 4:
                    createWorkout(ref workout4, 4, 5);
                    break;
                case 5:
                    createWorkout(ref workout5, 5, 6);
                    break;
                case 6:
                    createWorkout(ref workout6, 6, 7);
                    break;
                case 7:
                    for (int i = 0; i < exerciseList.Count; i++)
                    {
                        if (i! < exerciseList.Count - 1)
                        {
                            workout7 += exerciseList[i].ExerciseName + ",";
                        }
                        else workout7 += exerciseList[i].ExerciseName;
                    }
                    WorkoutDays.Add("Workout 7");
                    if (currentWorkoutNumber <= workoutNumber) currentWorkoutNumber++;
                    else if (currentWorkoutNumber == workoutNumber - 1) createNextButton.Text = "Done";
                    else if (currentWorkoutNumber == workoutNumber)
                    {
                        createPanel.Visible = false;
                        trainingDaysPanel.Visible = true;
                    }
                    break;
            }
        }

        private void chestTricepsShoulderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chestTricepsShoulderCheckBox.Checked)
            {
                frontCTS.Image = Properties.Resources.frontChestShouldersTricepsBordered;
                backCTS.Image = Properties.Resources.backChestShouldersTricepsBordered;
            }
            else
            {
                frontCTS.Image = Properties.Resources.frontChestShouldersTriceps;
                backCTS.Image = Properties.Resources.backChestShouldersTriceps;
            }
            if (!chestTricepsShoulderCheckBox.Checked && !backBicepsCheckBox.Checked)
            {
                upperBodyHomeOutdoorsButton.Visible = false;
            }
            else upperBodyHomeOutdoorsButton.Visible = true;
        }

        private void backBicepsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (backBicepsCheckBox.Checked)
            {
                frontBB.Image = Properties.Resources.frontBackBicepsBordererd;
                backBB.Image = Properties.Resources.backBackBicepsBordered;
            }
            else
            {
                frontBB.Image = Properties.Resources.frontBackBiceps;
                backBB.Image = Properties.Resources.backBackBiceps;
            }
            if (!chestTricepsShoulderCheckBox.Checked && !backBicepsCheckBox.Checked)
            {
                upperBodyHomeOutdoorsButton.Visible = false;
            }
            else upperBodyHomeOutdoorsButton.Visible = true;
        }

        private void frontCTS_Click(object sender, EventArgs e)
        {
            if (chestTricepsShoulderCheckBox.Checked)
            {
                backCTS.Image = Properties.Resources.backChestShouldersTricepsBordered;
                frontCTS.Image = Properties.Resources.frontChestShouldersTricepsBordered;
                chestTricepsShoulderCheckBox.Checked = false;
            }
            else
            {
                backCTS.Image = Properties.Resources.backChestShouldersTriceps;
                frontCTS.Image = Properties.Resources.frontChestShouldersTriceps;
                chestTricepsShoulderCheckBox.Checked = true;
            }
            if (!chestTricepsShoulderCheckBox.Checked && !backBicepsCheckBox.Checked)
            {
                upperBodyHomeOutdoorsButton.Visible = false;
            }
            else upperBodyHomeOutdoorsButton.Visible = true;
        }

        private void backCTS_Click(object sender, EventArgs e)
        {
            if (chestTricepsShoulderCheckBox.Checked)
            {
                backCTS.Image = Properties.Resources.backChestShouldersTricepsBordered;
                frontCTS.Image = Properties.Resources.frontChestShouldersTricepsBordered;
                chestTricepsShoulderCheckBox.Checked = false;
            }
            else
            {
                backCTS.Image = Properties.Resources.backChestShouldersTriceps;
                frontCTS.Image = Properties.Resources.frontChestShouldersTriceps;
                chestTricepsShoulderCheckBox.Checked = true;
            }
            if (!chestTricepsShoulderCheckBox.Checked && !backBicepsCheckBox.Checked)
            {
                upperBodyHomeOutdoorsButton.Visible = false;
            }
            else upperBodyHomeOutdoorsButton.Visible = true;
        }

        private void frontBB_Click(object sender, EventArgs e)
        {
            if (backBicepsCheckBox.Checked)
            {
                backBB.Image = Properties.Resources.backBackBicepsBordered;
                frontBB.Image = Properties.Resources.frontBackBicepsBordererd;
                backBicepsCheckBox.Checked = false;
            }
            else
            {
                backBB.Image = Properties.Resources.backBackBiceps;
                frontBB.Image = Properties.Resources.frontBackBiceps;
                backBicepsCheckBox.Checked = true;
            }
            if (!chestTricepsShoulderCheckBox.Checked && !backBicepsCheckBox.Checked)
            {
                upperBodyHomeOutdoorsButton.Visible = false;
            }
            else upperBodyHomeOutdoorsButton.Visible = true;
        }

        private void backBB_Click(object sender, EventArgs e)
        {
            if (backBicepsCheckBox.Checked)
            {
                backBB.Image = Properties.Resources.backBackBicepsBordered;
                frontBB.Image = Properties.Resources.frontBackBicepsBordererd;
                backBicepsCheckBox.Checked = false;
            }
            else
            {
                backBB.Image = Properties.Resources.backBackBiceps;
                frontBB.Image = Properties.Resources.frontBackBiceps;
                backBicepsCheckBox.Checked = true;
            }
            if (!chestTricepsShoulderCheckBox.Checked && !backBicepsCheckBox.Checked)
            {
                upperBodyHomeOutdoorsButton.Visible = false;
            }
            else upperBodyHomeOutdoorsButton.Visible = true;
        }

        private void upperBodyHomeOutdoorsButton_Click(object sender, EventArgs e)
        {
            if (!lowerBodyClicked)
            {
                upperBodyHomeOutdoorsPanel.Visible = false;
                chooseWorkoutNumberPanel.Visible = true;
            }
            else
            {
                upperBodyHomeOutdoorsPanel.Visible = false;
                lowerBodyPanel.Visible = true;
            }
        }
    }
}
