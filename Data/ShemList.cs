using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simul.Data
{
    public class ShemList
    {
        public static List<string> shemaLine = new List<string>
            {
                "pitanie_3F_L1_avtomat_3F_1", "avtomat_3F_1_pitanie_3F_L1",
            "pitanie_3F_L2_avtomat_3F_3", "avtomat_3F_3_pitanie_3F_L2",
            "pitanie_3F_L3_avtomat_3F_5", "avtomat_3F_5_pitanie_3F_L3",
                        "pitanie_3F_N_tepl_rel_96", "tepl_rel_96_pitanie_3F_N",

            "avtomat_3F_2_kontactor_1", "kontactor_1_avtomat_3F_2",
            "avtomat_3F_4_kontactor_3", "kontactor_3_avtomat_3F_4",
                        "avtomat_3F_6_kontactor_5", "kontactor_5_avtomat_3F_6",

            "tepl_rel_1_kontactor_2", "kontactor_2_tepl_rel_1",
            "tepl_rel_3_kontactor_4", "kontactor_4_tepl_rel_3",
                        "tepl_rel_5_kontactor_6", "kontactor_6_tepl_rel_5",

            "tepl_rel_2_electrodvigatel_U1", "electrodvigatel_U1_tepl_rel_2",
            "tepl_rel_4_4_electrodvigatel_V1", "electrodvigatel_V1_tepl_rel_4",
            "tepl_rel_6_electrodvigatel_W1", "electrodvigatel_W1_tepl_rel_6",

            "kontactor_A1_kontactor_13", "kontactor_13_kontactor_A1",
            "kontactor_A2_tepl_rel_95", "tepl_rel_95_kontactor_A2",

                        "post_SB2_13_kontactor_A1", "kontactor_A1_post_SB2_13",
            "post_SB2_14_kontactor_14", "kontactor_14_post_SB2_14",
            "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
            "post_SB2_22_kontactor_5", "kontactor_5_post_SB2_22"
            };

        public static List<string> shemaLineRevers = new List<string>
            {
                "pitanie_3F_L1_avtomat_3F_1", "avtomat_3F_1_pitanie_3F_L1",
            "pitanie_3F_L2_avtomat_3F_3", "avtomat_3F_3_pitanie_3F_L2",
            "pitanie_3F_L3_avtomat_3F_5", "avtomat_3F_5_pitanie_3F_L3",
                        "pitanie_3F_N_tepl_rel_96", "tepl_rel_96_pitanie_3F_N",

            "avtomat_3F_2_kontactor_1", "kontactor_1_avtomat_3F_2",
            "avtomat_3F_4_kontactor_3", "kontactor_3_avtomat_3F_4",
                        "avtomat_3F_6_kontactor_5", "kontactor_5_avtomat_3F_6",

            "tepl_rel_1_kontactor_2", "kontactor_2_tepl_rel_1",
            "tepl_rel_3_kontactor_4", "kontactor_4_tepl_rel_3",
                        "tepl_rel_5_kontactor_6", "kontactor_6_tepl_rel_5",

            "tepl_rel_6_electrodvigatel_U1", "electrodvigatel_U1_tepl_rel_6",
            "tepl_rel_4_4_electrodvigatel_V1", "electrodvigatel_V1_tepl_rel_4",
            "tepl_rel_2_electrodvigatel_W1", "electrodvigatel_W1_tepl_rel_2",

            "kontactor_A1_kontactor_13", "kontactor_13_kontactor_A1",
            "kontactor_A2_tepl_rel_95", "tepl_rel_95_kontactor_A2",

                        "post_SB2_13_kontactor_A1", "kontactor_A1_post_SB2_13",
            "post_SB2_14_kontactor_14", "kontactor_14_post_SB2_14",
            "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
            "post_SB2_22_kontactor_5", "kontactor_5_post_SB2_22"
            };

        public static List<string> shemaLine_Pit_Avt3F_PostSB2_Eldv = new List<string>
        {
                "pitanie_3F_L1_avtomat_3F_1", "avtomat_3F_1_pitanie_3F_L1",
                "pitanie_3F_L2_avtomat_3F_3", "avtomat_3F_3_pitanie_3F_L2",
                "pitanie_3F_L3_avtomat_3F_5", "avtomat_3F_5_pitanie_3F_L3",

                "avtomat_3F_2_electrodvigatel_U1", "electrodvigatel_U1_avtomat_3F_2",
                "avtomat_3F_4_electrodvigatel_V1", "electrodvigatel_V1_avtomat_3F_4",
                "avtomat_3F_6_post_SB2_22", "avtomat_3F_6_post_SB2_22",

                "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
                "electrodvigatel_W1_post_SB2_13", "post_SB2_13_electrodvigatel_W1"
        };
        public static List<string> shemaLine_Pit_Avt3F_PostSB2_Eldv_Revers = new List<string>
        {
                "pitanie_3F_L1_avtomat_3F_1", "avtomat_3F_1_pitanie_3F_L1",
                "pitanie_3F_L2_avtomat_3F_3", "avtomat_3F_3_pitanie_3F_L2",
                "pitanie_3F_L3_avtomat_3F_5", "avtomat_3F_5_pitanie_3F_L3",

                "avtomat_3F_2_electrodvigatel_W1", "electrodvigatel_W1_avtomat_3F_2",
                "avtomat_3F_4_electrodvigatel_V1", "electrodvigatel_V1_avtomat_3F_4",
                "avtomat_3F_6_post_SB2_22", "avtomat_3F_6_post_SB2_22",

                "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
                "electrodvigatel_U1_post_SB2_13", "post_SB2_13_electrodvigatel_U1"
        };

        // схема для проверки ошибочных соединений
        public static List<string> shemaMistake = new List<string>
        {
            /* pitanie      avtomat */
            "pitanie_3F_L1_avtomat_3F_1", "avtomat_3F_1_pitanie_3F_L1",
            "pitanie_3F_L2_avtomat_3F_3", "avtomat_3F_3_pitanie_3F_L2",
            "pitanie_3F_L3_avtomat_3F_5", "avtomat_3F_5_pitanie_3F_L3",
            "pitanie_3F_N_tepl_rel_96", "tepl_rel_96_pitanie_3F_N",

            /*  avtomat    post_SB2    electrodvigatel  */
            "avtomat_3F_2_electrodvigatel_U1", "electrodvigatel_U1_avtomat_3F_2",
                "avtomat_3F_4_electrodvigatel_V1", "electrodvigatel_V1_avtomat_3F_4",
                "avtomat_3F_6_post_SB2_22", "avtomat_3F_6_post_SB2_22",
                "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
                "electrodvigatel_W1_post_SB2_13", "post_SB2_13_electrodvigatel_W1",
            /*  avtomat    post_SB2    electrodvigatel    REVERS  */
            "avtomat_3F_2_electrodvigatel_W1", "electrodvigatel_W1_avtomat_3F_2",
                "avtomat_3F_4_electrodvigatel_V1", "electrodvigatel_V1_avtomat_3F_4",
                "avtomat_3F_6_post_SB2_22", "avtomat_3F_6_post_SB2_22",
                "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
                "electrodvigatel_U1_post_SB2_13", "post_SB2_13_electrodvigatel_U1",

            /* avtomat     kontactor */
            "avtomat_3F_2_kontactor_1", "kontactor_1_avtomat_3F_2",
            "avtomat_3F_4_kontactor_3", "kontactor_3_avtomat_3F_4",
            "avtomat_3F_6_kontactor_5", "kontactor_5_avtomat_3F_6",

            /*  kontactor         teplovoe_rele  */
            "tepl_rel_1_kontactor_2", "kontactor_2_tepl_rel_1",
            "tepl_rel_3_kontactor_4", "kontactor_4_tepl_rel_3",
            "tepl_rel_5_kontactor_6", "kontactor_6_tepl_rel_5",

            /*  kontactor  katushki*/
            "kontactor_A1_kontactor_13", "kontactor_13_kontactor_A1",
            "kontactor_A2_tepl_rel_95", "tepl_rel_95_kontactor_A2",

            /* post_SB2     kontactor  */
            "post_SB2_13_kontactor_A1", "kontactor_A1_post_SB2_13",
            "post_SB2_14_kontactor_14", "kontactor_14_post_SB2_14",
            "post_SB2_21_post_SB2_14", "post_SB2_14_post_SB2_21",
            "post_SB2_22_kontactor_5", "kontactor_5_post_SB2_22",

            /* teplovoe_rele    electrodvigatel */
            "tepl_rel_2_electrodvigatel_U1", "electrodvigatel_U1_tepl_rel_2",
            "tepl_rel_4_4_electrodvigatel_V1", "electrodvigatel_V1_tepl_rel_4",
            "tepl_rel_6_electrodvigatel_W1", "electrodvigatel_W1_tepl_rel_6",
            /* teplovoe_rele    electrodvigatel  REVERS */
            "tepl_rel_6_electrodvigatel_U1", "electrodvigatel_U1_tepl_rel_6",
            "tepl_rel_4_4_electrodvigatel_V1", "electrodvigatel_V1_tepl_rel_4",
            "tepl_rel_2_electrodvigatel_W1", "electrodvigatel_W1_tepl_rel_2",

        };
    }
}
