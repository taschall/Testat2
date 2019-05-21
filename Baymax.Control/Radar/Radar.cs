//------------------------------------------------------------------------------
// C #   I N   A C T I O N   ( C S A )
//------------------------------------------------------------------------------
// Repository:
//    $Id: Radar.cs 1039 2016-10-25 11:56:45Z chj-hslu $
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Baymax.Control
{
    public class Radar
    {

        #region members
        private int ioAddress;
        #endregion


        #region constructor & destructor
        public Radar(int IOAddress)
		{
            ioAddress = IOAddress;
        }
        #endregion


        #region properties
        /// <summary>
        /// Liefert die gemessene Distanz zum nächsten Objekt [m]
        /// </summary>
        public float Distance { get { return Baymax.Control.IOPort.Read(ioAddress) / 100.0f; } }
        #endregion

    }
}
