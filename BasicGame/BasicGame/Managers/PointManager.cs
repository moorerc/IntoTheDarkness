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
    /*
     * PointManager
     * 
     * Last Updated : Novemeber 21, 2012
     * v1.0 : Max - First implementation. PointManager
     * v2.0 : Max - Second implementation. PointManager2
     * v2.1 : Max - Added pulsing points. These points are re-added to the
     *              HashSet once they are removed to give a pulsing look.
     *              Used for switches/machines
     * v2.2 : Bryan - fixed index out of range bug. 
     * 
     * USAGE :
     * 
     * Keeps track of where contact points are. Contact points are either pulse
     * points or normal points. pulse points will pulse (i.e. will repeat)
     * 
     * IMPORTANT :
     * 
     * Nothing right now
     */

    public class Point_Time
    {
        private Vector4 point;
        private double time;
        private double init_time;
        private double prev_time;
        private double radius;

        public Point_Time(Vector4 v)
        {
            point = v;
            time = 0.0;
            init_time = 0.0;
            prev_time = 0.0;
            radius = 0.0f;
        }

        public Point_Time(Vector4 v, float t, float r)
        {
            point = v;
            time = t;
            init_time = t;
            prev_time = t;
            radius = r;
        }

        public Vector4 get_point() { return point; }
        public float get_radius() { return (float)radius; }

        public double set_time(float t)
        {
            //System.Diagnostics.Trace.WriteLine("time before = " + time);
            //System.Diagnostics.Trace.WriteLine("prev_time = " + prev_time);
            //System.Diagnostics.Trace.WriteLine("t = " + t);
            //System.Diagnostics.Trace.WriteLine("init = " + init_time);

            if (t >= prev_time)
                time = time + (t - prev_time);
            else
                time = time + (1000 - prev_time + t);

            prev_time = t;

            return time - init_time;
        }

        public void reset_time()
        {
            time = time - 900;
        }

        public double get_time() { return time - init_time; }

        public void tweak_time(Vector4 tweak)
        {
            point += tweak;
        }
    }

    public class Point_TimeComparer : IEqualityComparer<Point_Time>
    {
        public bool Equals(Point_Time one, Point_Time two)
        {
            // Adjust according to requirements
            return (one.get_point() == two.get_point());
        }

        public int GetHashCode(Point_Time item)
        {
            String s = item.ToString();
            return s.GetHashCode();
        }
    }
}