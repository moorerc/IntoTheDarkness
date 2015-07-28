using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EECS494.IntoTheDarkness
{
    public static class PointManagerRemake
    {
        // comparer
        static Point_TimeComparer comparer = new Point_TimeComparer();
        // lists to hold points
        static HashSet<Point_Time> m_points = new HashSet<Point_Time>(comparer);
        static HashSet<Point_Time> m_pulse_points = new HashSet<Point_Time>(comparer);
        // times and points array
        static float[] times = new float[8];
        static Vector4[] c_points = new Vector4[8];
        static float[] radiuses = new float[8];

        #region Accessors
        static public float[] Times
        {
            get
            {
                return times;
            }
        }

        static public Vector4[] ContactPoints
        {
            get
            {
                return c_points;
            }
        }

        static public float[] Radiuses
        {
            get
            {
                return radiuses;
            }
        }

        #endregion


        #region Adding points

        public static void add_Point(Vector4 point, float time, float rad)
        {
            //Point_Time p = new Point_Time(point);
            Point_Time p = new Point_Time(point, time, rad);
            while (!m_points.Add(p))  // only goes into the loop is the item is already there
            {
                Random r = new Random();
                Vector4 tweak = new Vector4((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), 0.0f);
                p.tweak_time(tweak);
            }
        }

        public static void add_Pulse_Point(Vector4 point, float time, float rad)
        {
            //Point_Time p = new Point_Time(point);
            Point_Time p = new Point_Time(point, time, rad);
            if (!m_pulse_points.Add(p))    // returns true if added. returns false is item is already present
            {
                m_pulse_points.Remove(p);
                m_pulse_points.Add(p);
            }
        }



        #endregion

        #region Removing pulse points and Clearing

        public static void ClearAll()
        {
            m_points.Clear();
            m_pulse_points.Clear();
        }

        public static void remove_Pulse_Point(Vector4 point)
        {
            Point_Time p = new Point_Time(point, 0.0f, 0.0f); // time and radius doesn't matter for comparison
            if (m_pulse_points.Contains(p))
            {
                m_pulse_points.Remove(p);
            }
        }

        #endregion

        #region Update

        public static void updateTime(float time)
        {
            List<Point_Time> pts = new List<Point_Time>();
            int index = 0;
            // update m_points
            foreach (Point_Time p in m_points)
            {
                if (p.set_time(time) >= 2000)
                {
                    pts.Add(p);
                    //m_points.Remove(p);
                }
                else if (index < 8) // set times and c_points
                {
                    times[index] = (float)p.get_time();
                    c_points[index] = p.get_point();
                    radiuses[index] = p.get_radius();
                    index++;
                }
            }

            foreach (Point_Time p in pts)
                m_points.Remove(p);

            pts.Clear();
            // update pulse_points
            foreach (Point_Time p in m_pulse_points)
            {
                if (p.set_time(time) >= 900)
                {
                    pts.Add(p);
                    //m_points.Remove(p);
                }

                // pulse points are never really removed, so thery are always
                // added to the array
                if (index < 8)
                {
                    times[index] = (float)p.get_time();
                    c_points[index] = p.get_point();
                    radiuses[index] = p.get_radius();
                    index++;
                }
            }

            foreach (Point_Time p in pts)
            {
                //double t = p.get_time();

                m_pulse_points.Remove(p);
                p.reset_time();
                m_pulse_points.Add(p);
            }

            // if index is not at 8, clear it out
            if (index < 8)
            {
                Vector4 far_point = new Vector4(-10000, -10000, -10000, -10000);
                float large_time = 10000;
                float small_radius = 0;
                while (index < 8)
                {
                    c_points[index] = far_point;
                    times[index] = large_time;
                    radiuses[index] = small_radius;
                    index++;
                }
            }
        }

        #endregion

    }


}