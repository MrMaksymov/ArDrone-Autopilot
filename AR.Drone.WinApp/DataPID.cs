using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.Drone.WinApp
{
    class DataPID
    {
        public float _integral_forwardnback = 0.0f;
        public float _integral_upndown = 0.0f;
        public float _integral_leftnright = 0.0f;

        public float _k_forwardnback_p = 0.0f;
        public float _k_forwardnback_d = 0.0f;
        public float _k_forwardnback_i = 0.0f;

        public float _k_leftnright_p = 0.0f;
        public float _k_leftnright_d = 0.0f;
        public float _k_leftnright_i = 0.0f;

        public float _k_upndown_p = 0.0f;
        public float _k_upndown_d = 0.0f;
        public float _k_upndown_i = 0.0f;

        public float _error_forwardnback = 0.0f;
        public float _derivative_forwardnback = 0.0f;

        public float _error_upndown = 0.0f;
        public float _derivative_upndown = 0.0f;

        public float _error_leftnright = 0.0f;
        public float _derivative_leftnright = 0.0f;

        public int _sample_length = 0;
        public float _sample_error = 0.0f;

        public uint _number_of_frame = 0;

        public int _time = 0;

    }
}
