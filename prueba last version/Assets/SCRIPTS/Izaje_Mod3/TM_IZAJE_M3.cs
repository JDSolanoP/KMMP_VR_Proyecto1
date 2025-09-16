using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TM_IZAJE_M3 : Lista_Tareas_Controller
{
    [SerializeField] private float tiempoEspera = 0f;
    [SerializeField] private GameObject[] TableroFails = null;
    [SerializeField] private GameObject[] TableroWins = null;
    [SerializeField] private GameObject[] TablerosOther = null;
    [SerializeField] private GameObject[] TablerosMini = null;
    [SerializeField] private GameObject[] GameObjects = null;
    [SerializeField] private GameObject[] Arrows = null;
    [SerializeField] private GameObject[] FloorArrows = null;
    [SerializeField] private GameObject[] FailArrows = null;
    [SerializeField] private GameObject DetectorConclsuiones = null;
    [SerializeField] private GameObject IniciarButton = null;

    [Header("Drenado de Aceite")]
    [SerializeField] private bool HasOilBeenDrainedFromSistemaDeTransmision = false;
    [SerializeField] private bool HasOilBeenDrainedFromFrenoDeServicio = false;
    [SerializeField] private GameObject CheckSistemaDeTransmision = null;
    [SerializeField] private GameObject CheckFrenoDeServicio = null;
    [SerializeField] private GameObject BandejaParaAceiteSistemaDeTransmision = null;
    [SerializeField] private GameObject BandejaParaAceiteFrenoDeServicio = null;
    [SerializeField] private GameObject BotonConformePanel1 = null;

    [Header("Ganchos de cadena")]
    [SerializeField] private GameObject Cadenas = null;
    [SerializeField] private HookObject HookObject1;
    [SerializeField] private HookObject HookObject2;
    [SerializeField] private Transform CargaIzable = null;
    [SerializeField] private Transform MotorElectrico = null;
    [SerializeField] private GameObject MotorElectricoWithRigidbody = null;
    [SerializeField] private GameObject ChainReference = null;
    [SerializeField] private GameObject LockerChainReference = null;
    [SerializeField] private GameObject[] ChainInterectable = null;
    [SerializeField] private GameObject[] ChainGrab = null;
    [SerializeField] private GameObject[] ChainMesh = null;
    [SerializeField] private GameObject[] ChainVerification = null;
    [SerializeField] private GameObject[] LockerEquipmentDoors = null;
    [SerializeField] private GameObject ComprobarCadenaConformeButton = null;
    private bool _hasCorrectChain = false;

    [Header("Pistola Neumática")]
    public bool Boquilla_ContactoRefe = false;
    public GameObject interruptorBTN_Compress;
    public float rotEncendido;
    public GameObject RefeinterruptorCompresora;
    public int TotalPernos;
    public int nPernosSacados;
    public GameObject ObjRefePernosConjunto;
    public GameObject[] PernosRefe;//imagen d referencia en verde
    public GameObject[] PernosGrab;//objeto agarrable
    public GameObject[] PernosMesh;//imagen de muestra inicial
    public float[] PernosTiempo;
    public float TiempoPernos;
    public ImpactWrenchGunAccionMod3 iwg;
    public GameObject PuntoPernoEnMaquina;
    public Vector3 LocalPosPernoSpawn;
    public Vector3 LocalRotPernoSpawn;
    public GameObject PuntoDeRecepccionPernos;
    public bool[] verificacionNPernos;
    public GameObject[] ComponentAnimLatigueo;//cable aparte solo activado para la animacion de latigueo
    public GameObject CableAnim;//cable para la animacion.
    public GameObject ObjJerarFinCable;//obj estacionario donde se ubicara el cable despues de desconectarlo
    public GameObject ObjConexionIWPCable;//boquilla de cable con pistola
    public GameObject CableCorrecto;//Cable que se usa siempre
    public GameObject ObjConexionIWPCableRefe;//capsula indicadora
    public GameObject ObjPuntoUbic_Pistola;//Punto en donde reubicar el punto reubicar la conexion
    public GameObject ObjConectorSiCorrecto;//Mesh si se realiza correctamente la desconexion
    public bool PistolaDescargada = false;
    public Vector3 conexIWPCalbePos0;
    public Vector3 conexIWPCalbeRot0;
    public bool ConectorMPPickedUp = false;
    public bool PernoEnDado = false;
    public bool[] PernoEnMano;
    public GameObject IWP_Refe;
    public GameObject IWP_Mesh;
    public GameObject IWP_OBJ;
    public GameObject ParticulasExpl;
    public GameObject Flecha_Indi;
    public GameObject[] ZonaDeBotador;
    bool si_algun_pernoEnMano = false;
    [SerializeField] private Transform PernoSacadoTransform = null;
    [SerializeField] private GameObject MuroParte1 = null;
    [SerializeField] private Transform Perno1MeshNewTransform = null;
    [SerializeField] private Transform Perno2MeshNewTransform = null;
    private bool _seSeparoElMotorElectrico = false;
    private bool _seUsoElBotador1 = false;
    private bool _seUsoElBotador2 = false;
    private int _botadorEnHueco0 = 2;
    private int _botadorEnHueco1 = 2;

    [Header("Traslado de Motor Electrico")]
    [SerializeField] private GameObject MotorElectricoReference = null;
    [SerializeField] private GameObject[] ChainHookTarea5 = null;
    [SerializeField] private GameObject AreasColisionMotorElectrico = null;
    [SerializeField] private GameObject BaseMaderaCircularParaMotorElectrico = null;
    [SerializeField] private GameObject ParedParaJugadorEnMotorElectrico = null;
    [SerializeField] private CargaIzable_ColViento[] CargasIzables_ColVientMotorElectrico = null;
    [SerializeField] private CargaIzable_Limites[] CargasIzables_LimitesMotorElectrico = null;
    [SerializeField] private GameObject CubosBloqueParaMotorElectricoLlegada = null;
    [SerializeField] private GameObject ControlPuenteGruaReferencia = null;

    [Header("Pernos")]
    [SerializeField] private GameObject[] PernosAgarrables = null;
    private bool _isPostTimeSkip = false;
    //public GameObject _ultimoPernoAgarrado = null;
    private bool _elAgarreDelPernoEstaDesactivado = false;
    [SerializeField] private GameObject MuroDePernos = null;
    public Transform[] _lastPernosBotadorPosition = null;
    private bool _isTheLastTimeThePernoWillBeUsed = false;
    private bool _isTheLastTimeThePerno2WillBeUsed = false;
    private int _pernosBotadoresUsados = 0;

    [Header("Guardado de elementos")]
    [SerializeField] private GameObject DesmontarCadenaButton = null;
    [SerializeField] private GameObject LeftHandleReference = null;
    [SerializeField] private GameObject RightHandleReference = null;
    private bool _isLeftDoorClosed = false;
    private bool _isRightDoorClosed = false;
    private bool _isChainInLocker = false;
    private bool _isGrillete1InLocker = false;
    private bool _isGrillete2InLocker = false;

    [Header("Elementos para el giro del Componente")]
    [SerializeField] private GameObject[] PatitasMesh = null;
    [SerializeField] private GameObject PatitaGrab = null;
    [SerializeField] private GameObject[] PatitasReferencia = null;
    [SerializeField] private GameObject[] OrejasMesh = null;
    [SerializeField] private GameObject OrejaGrab = null;
    [SerializeField] private GameObject[] OrejasReferencias = null;
    [SerializeField] private GameObject[] GrilletesMesh = null;
    [SerializeField] private GameObject[] GrilleteGrab = null;
    [SerializeField] private GameObject[] GrilletesReferencias = null;
    [SerializeField] private GameObject[] BushingMesh = null;
    [SerializeField] private GameObject BushingGrab = null;
    [SerializeField] private GameObject[] BushinReferencia = null;
    [SerializeField] private GameObject[] CheckBoxExtensiones = null;
    [SerializeField] private GameObject DetectorChoqueDeCadenas = null;
    private bool _isChainUnhooked1 = false;
    private bool _isChainUnhooked2 = false;

    [Header("Puente Grua")]
    [SerializeField] private GameObject ControlPuenteGrua = null;
    [SerializeField] private GameObject BotonesControlPuenteGrua = null;
    [SerializeField] private Control_Grua_Puente control_Grua_Puente = null;
    [SerializeField] private GameObject DetectoresDeMovimientoSinCorneta = null;
    private bool _seEquivocoAlPresionarCorneta = false;
    private Coroutine CheckSePresionoCornetaCoroutine = null;
    public bool _isCornetaPresionada = false;
    [SerializeField] private GameObject DetectoresDeMovimientoSinCornetaVueltaTarea5 = null;
    private Coroutine CheckSePresionoCornetaTarea5Coroutine = null;
    private bool _isCornetaPresionadaVueltaTarea5 = false;
    [SerializeField] private GameObject BloqueadorDeMovimientoDeMotorElectrico = null;
    [SerializeField] private GameObject BloqueadorDeMovimientoDeMotorElectricoLlegadaABase = null;
    [SerializeField] private GameObject BloqueadorDeMovimientoDeGanchoGrua = null;
    [SerializeField] private GameObject AreasColsionGanchoGrua = null;
    [SerializeField] private GameObject BloqueadorDeMovimientoDeGanchoGruaFijo = null;
    [SerializeField] private GameObject BloqueadorMTParaCadenas = null;
    private bool _seAgarroPorPrimeraVezElControl = false;

    [Header("Giro del Componente")]
    [SerializeField] private GameObject PuenteGruaReferenceTarea6 = null;

    //private bool _sePuedenMeterPernos = false;
    public GameObject _pernoBotador = null;
    [SerializeField] private Transform pernoBotadorPosition = null;
    [SerializeField] private Transform pernoBotadorPosition2 = null;
    //public bool _seObtuvoPernoBotador = false;

    [Header("Conclusiones y Final")]
    [SerializeField] private GameObject MuroBlockConclusiones = null;
    [SerializeField] private GameObject Muro2_Bloqueo = null;
    [SerializeField] private GameObject BotonContinuarConclusiones = null;
    [SerializeField] private GameObject PizarraConclusionesContinuarButton = null;
    [SerializeField] private GameObject PizarraFinalReiniciarButton = null;
    [SerializeField] private GameObject PizarraFinalSalirButton = null;
    [SerializeField] private GameObject[] PizarraChessFloor = null;

    [Header("Inicio")]
    [SerializeField] private Transform newSpawnPositionForPlayer = null;
    [SerializeField] private GameObject playerXROrigin = null;

    public override void Start()
    {
        base.Start();
        //StartCoroutine(ListaTareas(TareaActual));
        StartCoroutine(WaitForTheStartOfTheModule());
    }

    public override void TareaCompletada(int TareaSiguiente)
    {
        base.TareaCompletada(TareaSiguiente);
        StartCoroutine(ListaTareas(TareaActual));
    }

    private IEnumerator WaitForTheStartOfTheModule()
    {
        aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 1f, true);
        aSource.MusicaVol(1);

        for (int i = 0; i < Tablero_Indicaciones.Length; i++) Tablero_Indicaciones[i].SetActive(false);
        for (int i = 0; i < TableroWins.Length; i++) TableroWins[i].SetActive(false);
        for (int i = 0; i < TablerosMini.Length; i++) TablerosMini[i].SetActive(false);
        for (int i = 0; i < TablerosOther.Length; i++) TablerosOther[i].SetActive(false);
        for (int i = 0; i < TableroFails.Length; i++) TableroFails[i].SetActive(false);
        for (int i = 0; i < ChainGrab.Length; i++) ChainGrab[i].SetActive(false);
        for (int i = 0; i < PatitasMesh.Length; i++) PatitasMesh[i].SetActive(false);
        for (int i = 0; i < OrejasMesh.Length; i++) OrejasMesh[i].SetActive(false);
        for (int i = 0; i < BushingMesh.Length; i++) BushingMesh[i].SetActive(false);
        for (int i = 0; i < Arrows.Length; i++) Arrows[i].SetActive(false);
        for (int i = 0; i < FloorArrows.Length; i++) FloorArrows[i].SetActive(false);
        for (int i = 0; i < FailArrows.Length; i++) FailArrows[i].SetActive(false);
        for (int i = 0; i < PernosGrab.Length; i++) PernosGrab[i].SetActive(false);
        for (int i = 0; i < GrilletesReferencias.Length; i++) GrilletesReferencias[i].SetActive(false);
        for (int i = 0; i < PernosRefe.Length; i++) PernosRefe[i].SetActive(false);
        for (int i = 0; i < PizarraChessFloor.Length; i++) PizarraChessFloor[i].SetActive(false);

        PizarraConclusionesContinuarButton.SetActive(false);
        PizarraFinalReiniciarButton.SetActive(false);
        PizarraFinalSalirButton.SetActive(false);
        LeftHandleReference.SetActive(false);
        RightHandleReference.SetActive(false);
        ControlPuenteGruaReferencia.SetActive(false);
        AreasColisionMotorElectrico.SetActive(false);
        MotorElectricoReference.SetActive(false);
        PuenteGruaReferenceTarea6.SetActive(false);
        ControlPuenteGrua.GetComponent<One_Hand_PickUp>().enabled = false;
        ControlPuenteGrua.GetComponent<Control_Grua_Puente>().enabled = false;
        CubosBloqueParaMotorElectricoLlegada.SetActive(false);
        MuroDePernos.SetActive(false);
        LockerChainReference.SetActive(false);
        DesmontarCadenaButton.SetActive(false);
        IniciarButton.SetActive(false);
        Tablero_Indicaciones[0].SetActive(true);
        ComprobarCadenaConformeButton.SetActive(false);

        if (si_login)
        {
            while (TM_Lobby.lb.si_inicioModulo == false)
                yield return null;
        }
        float fadeTime = 1f;
        FadeOut(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        playerXROrigin.transform.position = newSpawnPositionForPlayer.position;
        playerXROrigin.transform.rotation = newSpawnPositionForPlayer.rotation;
        yield return new WaitForSeconds(0.125f);
        FadeIn(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(ListaTareas(TareaActual));
    }

    private IEnumerator ListaTareas(int tarea)
    {
        switch (tarea)
        {
            case 0: // Inicio - Bienvenida
                print($"Estás en la tarea {tarea}");
                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 1f, true);
                _lastPernosBotadorPosition = new Transform[2];

                for (int i = 0; i < Tablero_Indicaciones.Length; i++) Tablero_Indicaciones[i].SetActive(false);
                for (int i = 0; i < TableroWins.Length; i++) TableroWins[i].SetActive(false);
                for (int i = 0; i < TablerosMini.Length; i++) TablerosMini[i].SetActive(false);
                for (int i = 0; i < TablerosOther.Length; i++) TablerosOther[i].SetActive(false);
                for (int i = 0; i < TableroFails.Length; i++) TableroFails[i].SetActive(false);
                for (int i = 0; i < ChainGrab.Length; i++) ChainGrab[i].SetActive(false);
                for (int i = 0; i < PatitasMesh.Length; i++) PatitasMesh[i].SetActive(false);
                for (int i = 0; i < OrejasMesh.Length; i++) OrejasMesh[i].SetActive(false);
                for (int i = 0; i < BushingMesh.Length; i++) BushingMesh[i].SetActive(false);
                for (int i = 0; i < Arrows.Length; i++) Arrows[i].SetActive(false);
                for (int i = 0; i < FloorArrows.Length; i++) FloorArrows[i].SetActive(false);
                for (int i = 0; i < FailArrows.Length; i++) FailArrows[i].SetActive(false);
                for (int i = 0; i < PernosGrab.Length; i++) PernosGrab[i].SetActive(false);
                for (int i = 0; i < GrilletesReferencias.Length; i++) GrilletesReferencias[i].SetActive(false);
                for (int i = 0; i < PernosRefe.Length; i++) PernosRefe[i].SetActive(false);
                for (int i = 0; i < PizarraChessFloor.Length; i++) PizarraChessFloor[i].SetActive(false);

                PizarraConclusionesContinuarButton.SetActive(false);
                PizarraFinalReiniciarButton.SetActive(false);
                PizarraFinalSalirButton.SetActive(false);
                LeftHandleReference.SetActive(false);
                RightHandleReference.SetActive(false);
                ControlPuenteGruaReferencia.SetActive(false);
                AreasColisionMotorElectrico.SetActive(false);
                MotorElectricoReference.SetActive(false);
                PuenteGruaReferenceTarea6.SetActive(false);
                ControlPuenteGrua.GetComponent<One_Hand_PickUp>().enabled = false;
                ControlPuenteGrua.GetComponent<Control_Grua_Puente>().enabled = false;
                CubosBloqueParaMotorElectricoLlegada.SetActive(false);
                MuroDePernos.SetActive(false);
                LockerChainReference.SetActive(false);
                DesmontarCadenaButton.SetActive(false);
                IniciarButton.SetActive(false);
                Tablero_Indicaciones[0].SetActive(true);
                ComprobarCadenaConformeButton.SetActive(false);

                //Comentar lo de abajo para saltar la intro
                yield return new WaitForSeconds(2f);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                    print(AudioManager.aSource.IsPlayingVoz());
                }
                yield return new WaitForSeconds(0.1f);
                IniciarButton.SetActive(true);
                //Descomentar lo de arriba para versiones de muestra
                //TareaCompletada(0);

                iwg.gameObject.GetComponent<One_Hand_PickUp>().enabled = false;
                
                break;
            case 1: // Tarea por hacer: Drenar aceite - Termina al presionar el botón.
                print($"Estás en la tarea {tarea}");
                //yield return new WaitForSeconds(1f);
                Tablero_Indicaciones[0].SetActive(false);
                Tablero_Indicaciones[1].SetActive(true);
                FailArrows[3].SetActive(true);
                FailArrows[4].SetActive(true);
                yield return new WaitForSeconds(1f);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                    print(AudioManager.aSource.IsPlayingVoz());
                }
                BotonConformePanel1.SetActive(true);
                break;
            case 2: // Tarea por hacer: Seleccionar cadena y equipar - Termina al poner los ganchos en las orejas del motor eléctrico
                foreach (GameObject failArrow in FailArrows)
                {
                    failArrow.SetActive(false);
                }
                FloorArrows[0].SetActive(false);
                FloorArrows[1].SetActive(true);
                GrilleteGrab[0].GetComponent<One_Hand_PickUp>().enabled = true;
                GrilleteGrab[1].GetComponent<One_Hand_PickUp>().enabled = true;
                Arrows[1].SetActive(true);
                Arrows[2].SetActive(true);
                Arrows[15].SetActive(true);
                Arrows[16].SetActive(true);
                Arrows[3].SetActive(true);
                TablerosOther[1].SetActive(true);
                GrilletesReferencias[0].SetActive(true);
                GrilletesReferencias[3].SetActive(true);
                print($"Estás en la tarea {tarea}");
                BandejaParaAceiteFrenoDeServicio.SetActive(false);
                BandejaParaAceiteSistemaDeTransmision.SetActive(false);
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                SimpleActivateAndDeactivateTableros(tarea);
                break;
            case 3: // Tarea por hacer: Quitar el motor eléctrico - Termina cuando se usa el botador, el motor eléctrico se separa del MT y se desenergiza la pistola neumatica.
                TablerosMini[0].SetActive(false);
                TablerosMini[1].SetActive(true);
                PernosRefe[0].SetActive(true);
                iwg.gameObject.GetComponent<One_Hand_PickUp>().enabled = true;
                conexIWPCalbePos0 = ObjConexionIWPCable.transform.localPosition;
                conexIWPCalbeRot0 = ObjConexionIWPCable.transform.localEulerAngles;
                FloorArrows[1].SetActive(false);
                FloorArrows[2].SetActive(true);
                Arrows[4].SetActive(false);
                Arrows[5].SetActive(false);
                Arrows[6].SetActive(true);
                print($"Estás en la tarea {tarea}");
                //ActivarEvento(0);
                RefeinterruptorCompresora.SetActive(true);
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                TableroWins[0].SetActive(false);
                SimpleActivateAndDeactivateTableros(tarea);
                yield return new WaitForSeconds(1);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                    print(AudioManager.aSource.IsPlayingVoz());
                }
                aSource.PlayVoz("ParaFinesDidacticos");
                break;
            case 4: // Tarea por hacer: Trasladar motor eléctrico - Termina al dejar el motor eléctrico en la zona asignada.
                MuroParte1.SetActive(false);
                TableroFails[6].SetActive(false);
                AreasColisionMotorElectrico.SetActive(true);
                foreach (GameObject floorArrow in FloorArrows)
                {
                    floorArrow.SetActive(false);
                }
                FloorArrows[2].SetActive(false);
                FloorArrows[4].SetActive(true);
                CheckSePresionoCornetaCoroutine = StartCoroutine(CheckCornetaPresionadaPuenteGrua());
                print($"Estás en la tarea {tarea}");
                ControlPuenteGrua.GetComponent<One_Hand_PickUp>().enabled = true;
                ControlPuenteGrua.GetComponent<Control_Grua_Puente>().enabled = true;
                MotorElectricoReference.SetActive(true);
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                TablerosOther[0].SetActive(true);
                Tablero_Indicaciones[0].SetActive(false);
                TableroWins[1].SetActive(false);
                SimpleActivateAndDeactivateTableros(tarea);
                Arrows[0].SetActive(false);
                Arrows[7].SetActive(false);
                Arrows[9].SetActive(true);
                Arrows[10].SetActive(true);
                Arrows[14].SetActive(false);

                //yield return new WaitForSeconds(5f);
                break;
            case 5: // Tarea por hacer: Guardar accesorios del MT - Termina al cerrar las puertas e ir a la zona de conclusiones.
                BloqueadorDeMovimientoDeMotorElectricoLlegadaABase.SetActive(false);
                FloorArrows[5].SetActive(true);
                //CheckSePresionoCornetaTarea5Coroutine = StartCoroutine(CheckCornetaPresionadaPuenteGruaVueltaTarea5());
                LockerChainReference.SetActive(true);
                Arrows[12].SetActive(true);
                Arrows[13].SetActive(true);
                GrilletesReferencias[2].SetActive(true);
                GrilletesReferencias[4].SetActive(true);
                GrilletesReferencias[5].SetActive(true);

                ChainInterectable[2].SetActive(true);
                ChainInterectable[1].SetActive(true);
                ChainInterectable[2].GetComponent<One_Hand_PickUp>().enabled = false;
                ChainInterectable[1].GetComponent<One_Hand_PickUp>().enabled = false;

                //StartCoroutine(DeactivateGameObjectWithDelay(AreasColisionMotorElectrico, 5f));
                //BloqueadorDeMovimientoDeMotorElectrico.SetActive(true);

                //AreasColisionMotorElectrico.SetActive(false);
                //PuenteGruaReferenceTarea6.SetActive(true);
                print($"Estás en la tarea {tarea}");
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                TableroWins[2].SetActive(false);
                SimpleActivateAndDeactivateTableros(tarea);
                //Tablero_Indicaciones[7].SetActive(true);

                LockerEquipmentDoors[0].transform.localRotation = Quaternion.Euler(-90f, 0f, 25f);
                LockerEquipmentDoors[1].transform.localRotation = Quaternion.Euler(-90f, 0f, -62.5f);

                //PatitasReferencia[0].SetActive(true);
                //OrejasReferencias[0].SetActive(true);
                //BushinReferencia[0].SetActive(true);
                PatitaGrab.GetComponent<One_Hand_PickUp>().enabled = true;
                OrejaGrab.GetComponent<One_Hand_PickUp>().enabled = true;
                BushingGrab.GetComponent<One_Hand_PickUp>().enabled = true;
                ChainHookTarea5[0].GetComponent<One_Hand_PickUp>().enabled = true;
                ChainHookTarea5[1].GetComponent<One_Hand_PickUp>().enabled = true;
                Arrows[9].SetActive(false);
                //ControlPuenteGrua.GetComponent<Control_Grua_Puente>().enabled = false;
                //ControlPuenteGrua.GetComponent<Control_Grua_Puente>().StopAllCoroutines();
                //BotonesControlPuenteGrua.SetActive(false);
                //DetectorChoqueDeCadenas.SetActive(true);
                //MotorElectrico.parent = null;
                break;
            case 6: // Ir a conclusiones
                Tablero_Indicaciones[7].SetActive(false);
                
                //print($"Estás en la tarea {tarea}");
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                SimpleActivateAndDeactivateTableros(tarea);
                //Tablero_Indicaciones[6].SetActive(false);
                TableroWins[3].SetActive(false);
                break;
            
        }
        yield return new WaitForFixedUpdate();
    }


    private void SimpleActivateAndDeactivateTableros(int tareaActual)
    {
        Tablero_Indicaciones[tareaActual].SetActive(true);
        Tablero_Indicaciones[tareaActual - 1].SetActive(false);
        TableroWins[tareaActual - 2].SetActive(true);
    }

    

    public void ActivarEvento(int evento)
    {
        switch (evento)
        {
            case -3: // Drenar aceite del sistema de transmisión
                HasOilBeenDrainedFromSistemaDeTransmision = true;
                CheckSistemaDeTransmision.SetActive(true);
                FailArrows[3].SetActive(false);
                aSource.goFx("Bien");
                break;
            case -2: // Drenar aceite del freno de servicio
                HasOilBeenDrainedFromFrenoDeServicio = true;
                CheckFrenoDeServicio.SetActive(true);
                FailArrows[4].SetActive(false);
                aSource.goFx("Bien");
                break;
            case -1: // Cadena Correcta
                aSource.PlayVoz("PlaceholderLoquendo02");
                TablerosMini[0].SetActive(true);
                for (int i = 0; i < ChainInterectable.Length; i++)
                {
                    ChainInterectable[i].SetActive(false);
                }
                LockerEquipmentDoors[0].transform.localRotation = Quaternion.Euler(-90, 0, -90);
                LockerEquipmentDoors[1].transform.localRotation = Quaternion.Euler(-90, 0, 90);
                aSource.goFx("Bien");
                ChainVerification[0].SetActive(false);
                ChainReference.SetActive(false);
                ChainGrab[0].SetActive(true);
                ChainGrab[1].SetActive(true);
                ChainInterectable[0].SetActive(false);
                Arrows[3].SetActive(false);
                Arrows[4].SetActive(true);
                Arrows[5].SetActive(true);
                //Cadenas.SetActive(true);
                break;
            case 0:
                if (TareaActual == 3)
                {
                    if (nPernosSacados < TotalPernos)
                    {
                        //murosConos[2].SetActive(true);
                        aSource.goFx("Compresor_On", 0.5f, true, true);//*****************************************************************************SONIDO COMPRESORA*******************
                        iwg.MaquinaON_OFF(true);
                        iwg.DetectorPerno.SetActive(true);
                        Flecha_Indi.SetActive(false);
                        RefeinterruptorCompresora.SetActive(false);
                        interruptorBTN_Compress.transform.localEulerAngles = new Vector3(0, 0, rotEncendido);//debe ser angulo -65
                    }
                    else
                    {
                        aSource.altoFxLoop("Compresor_On");
                        aSource.goFx("Guardar_Pistola");
                        //aSource.FxAlto("Compresor_On");
                        iwg.MaquinaON_OFF(false);
                        RefeinterruptorCompresora.SetActive(false);
                        Flecha_Indi.SetActive(false);
                        interruptorBTN_Compress.transform.localEulerAngles = new Vector3(0, 0, 0);//debe ser angulo -65
                        ObjConexionIWPCableRefe.SetActive(true);
                        ObjConexionIWPCable.GetComponent<CapsuleCollider>().enabled = true;//Agregado el 16-07-24********

                    }
                }
                break;
            case 1:
                _pernoBotador.GetComponent<Return_Pos0>().enabled = false;
                _pernoBotador.GetComponent<One_Hand_PickUp>().enabled = true;
                break;
            case 2: // Desenganchar cadena 1
                if (TareaActual == 5)
                {
                    ChainHookTarea5[0].SetActive(false);
                    ChainMesh[0].SetActive(false);
                    ChainGrab[2].SetActive(true);
                    ChainGrab[2].transform.parent.gameObject.SetActive(true);
                    _isChainUnhooked1 = true;

                    GrilletesMesh[0].SetActive(false);
                    GrilleteGrab[1].SetActive(true);
                    GrilleteGrab[1].GetComponent<Rigidbody>().isKinematic = true;
                    GrilleteGrab[1].GetComponent<Rigidbody>().useGravity = false;
                    GrilleteGrab[1].transform.position = GrilletesMesh[0].transform.position;
                    GrilleteGrab[1].transform.rotation = GrilletesMesh[0].transform.rotation;
                    NewReturn_Pos0 grilleteNewReturnPos0 = GrilleteGrab[1].GetComponent<NewReturn_Pos0>();
                    grilleteNewReturnPos0.SetAuxTransform(GrilletesMesh[0].transform.position, GrilletesMesh[1].transform.eulerAngles);
                    grilleteNewReturnPos0.usarAuxPos0 = true;
                    grilleteNewReturnPos0.inGravKinec = true;

                    CheckIfMotorElectricoWasDisengaged();
                    aSource.goFx("Bien");
                }
                break;
            case 3: // Desenganchar cadena 2
                if (TareaActual == 5)
                {
                    ChainHookTarea5[1].SetActive(false);
                    ChainMesh[1].SetActive(false);
                    ChainGrab[3].SetActive(true);
                    ChainGrab[3].transform.parent.gameObject.SetActive(true);
                    _isChainUnhooked2 = true;

                    GrilletesMesh[1].SetActive(false);
                    GrilleteGrab[0].SetActive(true);
                    GrilleteGrab[0].GetComponent<Rigidbody>().isKinematic = true;
                    GrilleteGrab[0].GetComponent<Rigidbody>().useGravity = false;
                    GrilleteGrab[0].transform.position = GrilletesMesh[1].transform.position;
                    GrilleteGrab[0].transform.rotation = GrilletesMesh[1].transform.rotation;
                    NewReturn_Pos0 grilleteNewReturnPos0 = GrilleteGrab[0].GetComponent<NewReturn_Pos0>();
                    grilleteNewReturnPos0.SetAuxTransform(GrilletesMesh[1].transform.position, GrilletesMesh[1].transform.eulerAngles);
                    grilleteNewReturnPos0.usarAuxPos0 = true;
                    grilleteNewReturnPos0.inGravKinec = true;

                    CheckIfMotorElectricoWasDisengaged();
                    aSource.goFx("Bien");
                }
                break;
            case 4: // Quitar flechas del suelo para la persona y poner flechas del suelo para el camino del motor electrico
                if (!_seAgarroPorPrimeraVezElControl)
                {
                    foreach (GameObject floorArrow in FloorArrows)
                    {
                        floorArrow.SetActive(false);
                    }
                    FloorArrows[5].SetActive(false);
                    FloorArrows[6].SetActive(true);
                    _seAgarroPorPrimeraVezElControl = true;
                }
                break;
        }
    }

    public void verificarContacto(int confirmarContacto)
    {
        switch (confirmarContacto)
        {
            case 0: // Sacar perno
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    StartCoroutine(cronometro(confirmarContacto));
                }
                break;
            case 1: // La tarea se comepleta y se activa el detector en la boquilla de la pistola para poder meter pernos para usarlos como botadores
                if (PernoEnDado == false && si_algun_pernoEnMano == false && !_isPostTimeSkip)
                {
                    StartCoroutine(TimeSkipEffect());
                    //_sePuedenMeterPernos = true;
                    //iwg.SetDetectorPernoMeter(true);
                }
                if (TareaActual == 3 && _seSeparoElMotorElectrico && !si_algun_pernoEnMano && !PernoEnDado && !_isTheLastTimeThePernoWillBeUsed)
                {
                    //TareaCompletada(3);
                    nPernosSacados = 1;
                    Arrows[10].SetActive(false);
                    PernosGrab[0].GetComponent<One_Hand_PickUp>().enabled = false;
                    PernosGrab[0].GetComponent<CapsuleCollider>().enabled = false;
                    PernosGrab[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    NewReturn_Pos0 pernoNewReturnPos0 = PernosGrab[0].GetComponent<NewReturn_Pos0>();
                    pernoNewReturnPos0.SetAuxPos0(pernoBotadorPosition.position);
                    pernoNewReturnPos0.SetAuxRot0(pernoBotadorPosition.eulerAngles);
                    pernoNewReturnPos0.usarAuxPos0 = true;
                    pernoNewReturnPos0.inGravKinec = false;
                    PernosGrab[0].transform.position = pernoBotadorPosition.position;
                    PernosGrab[0].transform.rotation = pernoBotadorPosition.rotation;
                    //pernoNewReturnPos0.reposicionObj();
                    _isTheLastTimeThePernoWillBeUsed = true;
                    CheckPernosBotadoresEnContenedor();
                }
                break;
            case 2: // El perno se mete en la zona del botador
                //InsertPernoInGun(0);
                if (_isPostTimeSkip) //Funciona bien
                {
                    PernosGrab[0].SetActive(false);
                    PernosMesh[1].SetActive(true);
                    ZonaDeBotador[0].SetActive(false);
                    _botadorEnHueco0 = 0;
                    _lastPernosBotadorPosition[0] = PernosMesh[1].transform;
                }
                break;
            case 3: // Se usa la pistola neumatica en el perno botador
                if (contacto_confirmado[confirmarContacto] == true && _isPostTimeSkip)
                {
                    Debug.Log("El perno está en la zona del botador");
                    StartCoroutine(cronometro(1));
                }
                break;
            case 4: // Cuando el motor electrico llega a su zona
                //TareaCompletada(4);
                foreach (GameObject floorArrows in FloorArrows)
                {
                    floorArrows.SetActive(false);
                }
                aSource.goFx("Bien");
                if (si_login == true)
                {
                    TM_Lobby.lb.AgregarNota(3, TM_Lobby.lb.auxNotas[3]);
                    TM_Lobby.lb.GuardarNotasTotales();
                }
                CubosBloqueParaMotorElectricoLlegada.SetActive(false);
                BaseMaderaCircularParaMotorElectrico.SetActive(true);
                FloorArrows[4].SetActive(false);
                //FloorArrows[5].SetActive(true);
                BloqueadorDeMovimientoDeMotorElectricoLlegadaABase.SetActive(true);
                BloqueadorDeMovimientoDeMotorElectricoLlegadaABase.transform.SetParent(null);
                MotorElectricoReference.SetActive(false);
                ControlPuenteGruaReferencia.SetActive(true);
                aSource.PlayVoz("GuardeElControlDelPuenteGrua");
                break;
            case 5: // Cuando se inserta una patita
                aSource.goFx("Bien");
                StartCoroutine(RepeatSoundPistolaNeumatica(0.5f, 0.1f, 3));
                CheckBoxExtensiones[0].SetActive(true);
                CheckBoxExtensiones[4].SetActive(true);
                PatitaGrab.SetActive(false);
                PatitasMesh[0].SetActive(true);
                PatitasMesh[1].SetActive(true);
                PatitasReferencia[0].SetActive(false);
                break;
            case 6: // Cuando se coloca la cadena correcta
                aSource.PlayVoz("PlaceholderLoquendo01");
                _hasCorrectChain = true;
                ChainReference.SetActive(false);
                ChainVerification[0].SetActive(true);
                ChainInterectable[0].SetActive(false);
                ComprobarCadenaConformeButton.SetActive(true);
                //ActivarEvento(-1);
                break;
            case 7: // Cuando se coloca el grillete en el Motor Electrico, parte cercana al gavinete
                aSource.goFx("Bien");
                Arrows[1].SetActive(false);
                GrilletesReferencias[0].SetActive(false);
                GrilleteGrab[0].SetActive(false);
                GrilletesMesh[0].SetActive(true);
                if (GrilletesMesh[0].activeSelf && GrilletesMesh[1].activeSelf)
                {
                    Arrows[3].SetActive(true);
                    ChainReference.SetActive(true);
                    TablerosOther[2].SetActive(true);
                    aSource.PlayVoz("ComprobacionDeCadena");
                }
                break;
            case 8: // Cuando se inserta la oreja
                aSource.goFx("Bien");
                StartCoroutine(RepeatSoundPistolaNeumatica(0.5f, 0.1f, 4));
                CheckBoxExtensiones[2].SetActive(true);
                CheckBoxExtensiones[6].SetActive(true);
                OrejasReferencias[0].SetActive(false);
                OrejasMesh[0].SetActive(true);
                OrejasMesh[1].SetActive(true);
                OrejaGrab.SetActive(false);
                GrilletesReferencias[1].SetActive(true);
                break;
            case 9: // Cuando se inserta el bushing
                aSource.goFx("Bien");
                StartCoroutine(RepeatSoundPistolaNeumatica(0.5f, 0.1f, 9));
                CheckBoxExtensiones[1].SetActive(true);
                CheckBoxExtensiones[5].SetActive(true);
                BushinReferencia[0].SetActive(false);
                for (int i = 0;  i < BushingMesh.Length; i++)
                {
                    BushingMesh[i].SetActive(true);
                }
                BushingGrab.SetActive(false);
                break;
            case 10: // Cuando se inserta el grillete en el MT
                aSource.goFx("Bien");
                CheckBoxExtensiones[3].SetActive(true);
                CheckBoxExtensiones[7].SetActive(true);
                GrilletesReferencias[1].SetActive(false);
                GrilletesMesh[2].SetActive(true);
                GrilletesMesh[3].SetActive(true);
                GrilleteGrab[0].SetActive(false);
                break;
            case 11: // Cuando se inserta la cadena de 2 ton
                aSource.PlayVoz("PlaceholderLoquendo01");
                ChainReference.SetActive(false);
                ChainVerification[1].SetActive(true);
                ChainInterectable[1].SetActive(false);
                ComprobarCadenaConformeButton.SetActive(true);
                //EventoFail(1);
                _hasCorrectChain = false;
                break;
            case 12: // Cuando se inserta la cadena de 3 ton
                aSource.PlayVoz("PlaceholderLoquendo01");
                ChainReference.SetActive(false);
                ChainVerification[2].SetActive(true);
                ChainInterectable[2].SetActive(false);
                ComprobarCadenaConformeButton.SetActive(true);
                //EventoFail(1);
                _hasCorrectChain = false;
                break;
            case 13: // Cuando se mueve el componente sin presionar la corneta.
                if (!_isCornetaPresionada)
                {
                    EventoFail(2);
                    StopCoroutine(CheckSePresionoCornetaCoroutine);
                    DetectoresDeMovimientoSinCorneta.SetActive(false);
                }
                break;
            case 14: // Cuando las cadenas chocan con el MT
                if (TareaActual == 5)
                {
                    EventoFail(3);
                }
                break;
            case 15: // Cuando el gancho del puente grua llega al lugar indicado
                aSource.goFx("Bien");
                //BotonesControlPuenteGrua.SetActive(false);
                //ControlPuenteGrua.GetComponent<Control_Grua_Puente>().enabled = false;
                PuenteGruaReferenceTarea6.SetActive(false);
                BloqueadorDeMovimientoDeGanchoGruaFijo.SetActive(true);
                TableroFails[3].gameObject.SetActive(false);
                DetectorChoqueDeCadenas.SetActive(false);
                FloorArrows[3].SetActive(false);
                BloqueadorMTParaCadenas.SetActive(false);
                break;
            case 16: // Cuando se mueve las cadenas sin presionar la corneta
                if (TareaActual == 5 && !_isCornetaPresionadaVueltaTarea5)
                {
                    EventoFail(4);
                    StopCoroutine(CheckSePresionoCornetaTarea5Coroutine);
                    DetectoresDeMovimientoSinCornetaVueltaTarea5.SetActive(false);
                }
                break;
            case 17: // Cuando se inserta el grillete (2) mas cercano al pasillo
                aSource.goFx("Bien");
                Arrows[16].SetActive(false);              
                GrilletesReferencias[3].SetActive(false);
                GrilleteGrab[1].SetActive(false);
                GrilletesMesh[1].SetActive(true);                
                if (GrilletesMesh[0].activeSelf && GrilletesMesh[1].activeSelf)
                {
                    Arrows[3].SetActive(true);
                    ChainReference.SetActive(true);
                    TablerosOther[2].SetActive(true);
                    aSource.PlayVoz("ComprobacionDeCadena");
                }
                break;
            case 18: // Cuando se coloca el grillete (2) en el Motor Electrico, parte cercana al gavinete
                aSource.goFx("Bien");
                Arrows[1].SetActive(false);
                GrilletesReferencias[0].SetActive(false);
                GrilleteGrab[1].SetActive(false);
                GrilletesMesh[0].SetActive(true);
                if (GrilletesMesh[0].activeSelf && GrilletesMesh[1].activeSelf)
                {
                    Arrows[3].SetActive(true);
                    ChainReference.SetActive(true);
                    TablerosOther[2].SetActive(true);
                }
                break;
            case 19: // Cuando se coloca el grillete mas cercano al pasillo
                aSource.goFx("Bien");
                Arrows[16].SetActive(false);
                GrilletesReferencias[3].SetActive(false);
                GrilleteGrab[0].SetActive(false);
                GrilletesMesh[1].SetActive(true);
                if (GrilletesMesh[0].activeSelf && GrilletesMesh[1].activeSelf)
                {
                    Arrows[3].SetActive(true);
                    ChainReference.SetActive(true);
                    TablerosOther[2].SetActive(true);
                }
                break;
            case 20:
                if (contacto_confirmado[confirmarContacto] == false && ConectorMPPickedUp == true)
                {
                    if (iwg.Cargada == true)
                    {
                        Debug.Log("contacto manglera " + (confirmarContacto) + " en funcion verificar contacto desconectar de pistola de forma incorrecta");
                        animLatigueoIWG(iwg.Cargada);
                    }
                    else
                    {
                        Debug.Log("contacto manglera " + (confirmarContacto) + " en funcion verificar contacto desconectar de pistola de forma correcta");
                        animLatigueoIWG(iwg.Cargada);//ac
                        IWP_Refe.SetActive(true);
                    }

                }
                break;
            case 21:
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    IWP_Refe.SetActive(false);
                    IWP_Mesh.SetActive(true);
                    IWP_OBJ.SetActive(false);
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    aSource.goFx("Bien");
                    TareaCompletada(3);
                }
                break;
            case 22:
                //verificarContacto(1);
                if (!PernoEnDado && !si_algun_pernoEnMano && TareaActual == 3 && _isPostTimeSkip)
                {
                    StartCoroutine(SoltarPerno(0));
                }
                break;
            case 23:
                ControlPuenteGruaReferencia.SetActive(false);
                ControlPuenteGrua.GetComponent<One_Hand_PickUp>().enabled = false;
                ControlPuenteGrua.transform.position = ControlPuenteGruaReferencia.transform.position;
                ControlPuenteGrua.transform.rotation = ControlPuenteGruaReferencia.transform.rotation;
                Rigidbody controlPuenteGruaRigidbody = ControlPuenteGrua.GetComponent<Rigidbody>();
                controlPuenteGruaRigidbody.isKinematic = false;
                controlPuenteGruaRigidbody.useGravity = true;
                controlPuenteGruaRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                activarCtrlBTN(false);
                AreasColisionMotorElectrico.SetActive(false);
                TareaCompletada(4);
                break;
            case 24:
                GrilletesReferencias[4].SetActive(false);
                GrilleteGrab[0].SetActive(false);
                GrilletesMesh[0].transform.position = GrilletesReferencias[4].transform.position;
                GrilletesMesh[0].transform.rotation = GrilletesReferencias[4].transform.rotation;
                GrilletesMesh[0].SetActive(true);
                aSource.goFx("Bien");
                _isGrillete1InLocker = true;
                CheckTarea5();
                break;
            case 25:
                GrilletesReferencias[4].SetActive(false);
                GrilleteGrab[1].SetActive(false);
                GrilletesMesh[0].transform.position = GrilletesReferencias[4].transform.position;
                GrilletesMesh[0].transform.rotation = GrilletesReferencias[4].transform.rotation;
                GrilletesMesh[0].SetActive(true);
                aSource.goFx("Bien");
                _isGrillete1InLocker = true;
                CheckTarea5();
                break;
            case 26:
                GrilletesReferencias[5].SetActive(false);
                GrilleteGrab[0].SetActive(false);
                GrilletesMesh[1].transform.position = GrilletesReferencias[5].transform.position;
                GrilletesMesh[1].transform.rotation = GrilletesReferencias[5].transform.rotation;
                GrilletesMesh[1].SetActive(true);
                aSource.goFx("Bien");
                _isGrillete2InLocker = true;
                CheckTarea5();
                break;
            case 27:
                GrilletesReferencias[5].SetActive(false);
                GrilleteGrab[1].SetActive(false);
                GrilletesMesh[1].transform.position = GrilletesReferencias[5].transform.position;
                GrilletesMesh[1].transform.rotation = GrilletesReferencias[5].transform.rotation;
                GrilletesMesh[1].SetActive(true);
                aSource.goFx("Bien");
                _isGrillete2InLocker = true;
                CheckTarea5();
                break;
            case 28:
                LockerChainReference.SetActive(false);
                ChainInterectable[0].GetComponent<One_Hand_PickUp>().enabled = false;
                ChainInterectable[0].transform.position = LockerChainReference.transform.position;
                ChainInterectable[0].GetComponent<NewReturn_Pos0>().usarAuxPos0 = false;                
                DesmontarCadenaButton.SetActive(false);
                _isChainInLocker = true;
                aSource.goFx("Bien");
                CheckTarea5();
                break;
            case 29:
                Tablero_Indicaciones[6].SetActive(true);
                MuroBlockConclusiones.SetActive(false);
                StartCoroutine(SetActivateGameObjectAfterIsPlayingVoz(PizarraConclusionesContinuarButton, true));
                DetectorConclsuiones.SetActive(false);
                TareaCompletada(5);
                break;
            case 30:
                if (_isPostTimeSkip)
                {
                    PernosGrab[0].SetActive(false);
                    PernosMesh[3].SetActive(true);
                    ZonaDeBotador[1].SetActive(false);
                    _botadorEnHueco1 = 0;
                    _lastPernosBotadorPosition[1] = PernosMesh[5].transform;
                }
                break;
            case 31:
                if (contacto_confirmado[confirmarContacto] == true && _isPostTimeSkip)
                {
                    Debug.Log("El perno está en la zona del botador");
                    StartCoroutine(cronometro(2));
                }
                break;
            case 32:
                if (_isPostTimeSkip)
                {
                    PernosGrab[1].SetActive(false);
                    PernosMesh[1].SetActive(true);
                    ZonaDeBotador[0].SetActive(false);
                    _botadorEnHueco0 = 1;
                    _lastPernosBotadorPosition[0] = PernosMesh[1].transform;
                }
                break;
            case 33: //Funciona bien
                if (_isPostTimeSkip)
                {
                    PernosGrab[1].SetActive(false);
                    PernosMesh[3].SetActive(true);
                    ZonaDeBotador[1].SetActive(false);
                    _botadorEnHueco1 = 1;
                    _lastPernosBotadorPosition[1] = PernosMesh[5].transform;
                }
                break;
            case 34:
                if (TareaActual == 3 && _seSeparoElMotorElectrico && !si_algun_pernoEnMano && !PernoEnDado && !_isTheLastTimeThePerno2WillBeUsed)
                {
                    nPernosSacados = 1;
                    Arrows[10].SetActive(false);
                    PernosGrab[1].GetComponent<One_Hand_PickUp>().enabled = false;
                    PernosGrab[1].GetComponent<CapsuleCollider>().enabled = false;
                    PernosGrab[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    NewReturn_Pos0 pernoNewReturnPos0 = PernosGrab[1].GetComponent<NewReturn_Pos0>();
                    pernoNewReturnPos0.SetAuxPos0(pernoBotadorPosition.position);
                    pernoNewReturnPos0.SetAuxRot0(pernoBotadorPosition.eulerAngles);
                    pernoNewReturnPos0.usarAuxPos0 = true;
                    pernoNewReturnPos0.inGravKinec = false;
                    PernosGrab[1].transform.position = pernoBotadorPosition2.position;
                    PernosGrab[1].transform.rotation = pernoBotadorPosition2.rotation;
                    //pernoNewReturnPos0.reposicionObj();
                    _isTheLastTimeThePerno2WillBeUsed = true;
                    CheckPernosBotadoresEnContenedor();
                }
                break;
            case 35:
                if (!PernoEnDado && !si_algun_pernoEnMano && TareaActual == 3 && _isPostTimeSkip)
                {
                    StartCoroutine(SoltarPerno(1));
                }
                break;
        }
    }

    private void CheckPernosBotadoresEnContenedor()
    {
        if (_isTheLastTimeThePernoWillBeUsed && _isTheLastTimeThePerno2WillBeUsed)
        {
            Flecha_Indi.SetActive(true);
            RefeinterruptorCompresora.SetActive(true);
            Arrows[14].SetActive(false);
            //aSource.PlayVoz("DesenergiceLaPistola");
        }
    }
    private IEnumerator SoltarPerno(int numeroDePerno)
    {
        if (!_elAgarreDelPernoEstaDesactivado)
        {
            _elAgarreDelPernoEstaDesactivado = true;
            PernosGrab[numeroDePerno].GetComponent<One_Hand_PickUp>().enabled = false;
            verificarContacto(1);
            yield return new WaitForSeconds(0.1f);
            PernosGrab[numeroDePerno].GetComponent<NewReturn_Pos0>().reposicionObj();
            yield return new WaitForSeconds(0.1f);
            PernosGrab[numeroDePerno].GetComponent<One_Hand_PickUp>().enabled = true;
            if (TareaActual == 3 && _seSeparoElMotorElectrico && !si_algun_pernoEnMano && !PernoEnDado)
            {
                PernosGrab[numeroDePerno].GetComponent<One_Hand_PickUp>().enabled = false;
                if (numeroDePerno == 0)
                {
                    PernosGrab[numeroDePerno].transform.position = pernoBotadorPosition.position;
                    PernosGrab[numeroDePerno].transform.rotation = pernoBotadorPosition.rotation;
                }
                if (numeroDePerno == 1)
                {
                    PernosGrab[numeroDePerno].transform.position = pernoBotadorPosition2.position;
                    PernosGrab[numeroDePerno].transform.rotation = pernoBotadorPosition2.rotation;
                }
            }
            _elAgarreDelPernoEstaDesactivado = false;
        }
        
    }

    public void animLatigueoIWG(bool si_descargada)//cuando se detecta la desconexion de la manguera y la pistola
    {
        //CablePuntoFin.transform.SetParent(ObjJerarFinCable.transform);
        if (iwg.Cargada == false)
        {
            aSource.goFx("Bien");
            aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
            if (si_login == true)
            {
                TM_Lobby.lb.AgregarNota(2, TM_Lobby.lb.auxNotas[2]);
            }
            CableCorrecto.SetActive(false);
            ObjConexionIWPCable.SetActive(false);
            ObjConexionIWPCableRefe.SetActive(false);
            //Tablero_Indicaciones[11].SetActive(false);//panel correcto
            //Tablero_Indicaciones[12].SetActive(true);//panel correcto
            ObjConectorSiCorrecto.SetActive(true);
            Debug.Log("Desconexion correcta");
        }
        else
        {//SI NO Descarga
            aSource.goFx("ExplosionAire");////////////////////////////////////////////////////////**************************************SONIDO EXPLOSION*******************
            ObjConexionIWPCable.SetActive(false);
            CableCorrecto.SetActive(false);
            Debug.Log("animacion de latigueo");
            StartCoroutine(AnimLatigueoFull());
            Debug.Log("FIN DE animacion de latigueo");

            ObjConexionIWPCable.transform.SetParent(ObjJerarFinCable.transform);
            Debug.Log("ObjJerarFinCable=" + ObjJerarFinCable.name);
            ObjConexionIWPCable.transform.localPosition = conexIWPCalbePos0;
            ObjConexionIWPCable.transform.localEulerAngles = conexIWPCalbeRot0;
            Debug.Log("ObjConexionIWPCable.transform.localPosition.x " + ObjConexionIWPCable.transform.localPosition.x + " y " + ObjConexionIWPCable.transform.localPosition.y + " z " + ObjConexionIWPCable.transform.localPosition.z);
            ObjConexionIWPCable.GetComponent<CapsuleCollider>().enabled = false;
            ObjConexionIWPCableRefe.SetActive(false);
            //Tablero_Indicaciones[11].SetActive(true);//panel fallido
            aSource.goFx("Fallo");
            aSource.goFx("Locu_Fallo");//********************AGREGADO EL 27-08-24********************////////////
            Debug.Log("Desconexion incorrecta");
            if (si_login == true)
            {
                TM_Lobby.lb.auxNotas[2]++;
            }
            //TM_Lobby.lb.auxNotas[3]++;
        }

    }

    IEnumerator AnimLatigueoFull()
    {
        for (int i = 0; i < ComponentAnimLatigueo.Length; i++)
        {
            ComponentAnimLatigueo[i].SetActive(true);
        }
        ParticulasExpl.SetActive(true);
        Debug.Log("animacion realizando el latigueo");
        yield return new WaitForSeconds(1.5f);
        ParticulasExpl.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("FIN DE animacion de latigueo");
        for (int i = 0; i < ComponentAnimLatigueo.Length; i++)
        {
            ComponentAnimLatigueo[i].SetActive(false);
        }
        CableCorrecto.SetActive(true);
        ObjConexionIWPCable.SetActive(true);
        TableroFails[6].SetActive(true);
    }

    private IEnumerator cronometro(int contactoV)
    {
        switch (contactoV)
        {
            case 0://*********PARA IMPACT WRENCH GUN****************************Perno000//
                if (contacto_confirmado[contactoV] == true && PernoEnDado == false)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[contactoV] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        //Debug.Log("sacando perno "+(contactoV-1)+" tiempoxPernoActual = " + PernosTiempo[contactoV-1]);
                        if (iwg.si_presionando == false)
                        {
                            //aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[0] += 0.25f;
                        /*PernosTiempo[2] = 0;
                        PernosTiempo[1] = 0;
                        PernosTiempo[3] = 0;*/
                        if (tiempoEspera <= PernosTiempo[contactoV])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            PernosMesh[contactoV].SetActive(false);
                            PernosRefe[contactoV].SetActive(false);
                            PernosGrab[contactoV].SetActive(true);
                            PernosGrab[contactoV].GetComponent<NewReturn_Pos0>().SetTransform(PernosMesh[0].transform.position, PernosMesh[0].transform.eulerAngles);
                            //PernosGrab[contactoV].GetComponent<NewReturn_Pos0>().enabled = false;
                            PernosGrab[contactoV].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                            Boquilla_ContactoRefe = true;
                            Debug.Log("sacando perno y verificando el sonido" + (contactoV));
                            AudioManager.aSource.altoFxLoop("IWG_Rot01");
                            AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                            Arrows[22].SetActive(true);
                            Arrows[6].SetActive(false);
                            PernosRefe[0].SetActive(false);
                            PernosGrab[0].GetComponent<NewReturn_Pos0>().SetTransform(PernoSacadoTransform.position, PernoSacadoTransform.eulerAngles);

                            break;
                        }
                    }
                }
                else
                {
                    Boquilla_ContactoRefe = false;
                    //aSource.altoFx("IWG_Rot02");
                }
                break;

            case 1:
                /*if (contacto_confirmado[3] == true && PernoEnDado == true && _sePuedenMeterPernos)
                {
                    while (contacto_confirmado[3] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            break;
                        }
                        PernosTiempo[1] += 0.25f;

                        if (tiempoEspera <= PernosTiempo[contactoV])
                        {
                            _pernoBotador.transform.SetParent(ZonaDeBotador[0].transform);

                            PernoEnDado = false;
                            Boquilla_ContactoRefe = false;
                            _pernoBotador.transform.localPosition = LocalPosPernoSpawn;
                            _pernoBotador.transform.localEulerAngles = LocalRotPernoSpawn;

                            TareaCompletada(3);
                            EjectComponet();
                        }
                    }
                }
                break;*/
                if (contacto_confirmado[3] == true && PernoEnDado == false)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[3] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        //Debug.Log("sacando perno "+(contactoV-1)+" tiempoxPernoActual = " + PernosTiempo[contactoV-1]);
                        if (iwg.si_presionando == false)
                        {
                            //aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[1] += 0.25f;
                        /*PernosTiempo[2] = 0;
                        PernosTiempo[1] = 0;
                        PernosTiempo[3] = 0;*/
                        if (tiempoEspera <= PernosTiempo[1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            //PernosMesh[1].SetActive(false);
                            PernosMesh[1].transform.position = Perno1MeshNewTransform.position;
                            PernosMesh[1].SetActive(false);
                            if (!_seUsoElBotador1 && !_seUsoElBotador2) PernosMesh[4].SetActive(true);
                            //PernosRefe[contactoV].SetActive(false);
                            if (_botadorEnHueco0 == 0)
                            {
                                /*PernosGrab[0].SetActive(true);
                                PernosGrab[0].GetComponent<NewReturn_Pos0>().enabled = false;
                                PernosGrab[0].transform.SetParent(PuntoPernoEnMaquina.transform);
                                PernosGrab[0].transform.localPosition = LocalPosPernoSpawn;
                                PernosGrab[0].transform.localEulerAngles = LocalRotPernoSpawn;
                                PernosGrab[0].GetComponent<BoxCollider>().enabled = false;
                                PernosGrab[0].GetComponent<Rigidbody>().isKinematic = true;
                                PernosGrab[0].GetComponent<Rigidbody>().useGravity = false;
                                PernoEnMano[0] = false;
                                PernoEnDado = true;
                                Boquilla_ContactoRefe = true;*/
                                Debug.Log("sacando perno y verificando el sonido" + (contactoV));
                                AudioManager.aSource.altoFxLoop("IWG_Rot01");
                                //AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                                //Arrows[14].SetActive(true);
                                PernosRefe[1].SetActive(false);
                                //_lastPernosBotadorPosition[0] = PernosMesh[1].transform;
                                _seUsoElBotador1 = true;
                                Arrows[7].SetActive(false);
                                if (_pernosBotadoresUsados == 0)
                                {
                                    MotorElectrico.position = new Vector3(-1.7815f, MotorElectrico.position.y, MotorElectrico.position.z);
                                    _pernosBotadoresUsados = 1;
                                }
                                //PernosGrab[0].GetComponent<One_Hand_PickUp>().enabled = false;
                                EjectComponent();
                                break;
                            }
                            if (_botadorEnHueco0 == 1)
                            {
                                /*PernosGrab[1].SetActive(true);
                                PernosGrab[1].GetComponent<NewReturn_Pos0>().enabled = false;
                                PernosGrab[1].transform.SetParent(PuntoPernoEnMaquina.transform);
                                PernosGrab[1].transform.localPosition = LocalPosPernoSpawn;
                                PernosGrab[1].transform.localEulerAngles = LocalRotPernoSpawn;
                                PernosGrab[1].GetComponent<BoxCollider>().enabled = false;
                                PernosGrab[1].GetComponent<Rigidbody>().isKinematic = true;
                                PernosGrab[1].GetComponent<Rigidbody>().useGravity = false;
                                PernoEnMano[1] = false;
                                PernoEnDado = true;
                                Boquilla_ContactoRefe = true;*/
                                Debug.Log("sacando perno y verificando el sonido" + (contactoV));
                                AudioManager.aSource.altoFxLoop("IWG_Rot01");
                                //AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                                //Arrows[14].SetActive(true);
                                PernosRefe[1].SetActive(false);
                                //_lastPernosBotadorPosition[1] = PernosMesh[1].transform;
                                _seUsoElBotador1 = true;
                                Arrows[7].SetActive(false);
                                if (_pernosBotadoresUsados == 0)
                                {
                                    MotorElectrico.position = new Vector3(-1.7815f, MotorElectrico.position.y, MotorElectrico.position.z);
                                    _pernosBotadoresUsados = 1;
                                }
                                //PernosGrab[1].GetComponent<One_Hand_PickUp>().enabled = false;
                                EjectComponent();
                                break;
                            }
                            
                        }
                    }
                }
                else
                {
                    Boquilla_ContactoRefe = false;
                    //aSource.altoFx("IWG_Rot02");
                }
                break;
            case 2:
                if (contacto_confirmado[31] == true && PernoEnDado == false)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[31] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        //Debug.Log("sacando perno "+(contactoV-1)+" tiempoxPernoActual = " + PernosTiempo[contactoV-1]);
                        if (iwg.si_presionando == false)
                        {
                            //aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[2] += 0.25f;
                        /*PernosTiempo[2] = 0;
                        PernosTiempo[1] = 0;
                        PernosTiempo[3] = 0;*/
                        if (tiempoEspera <= PernosTiempo[2])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            //PernosMesh[3].SetActive(false);
                            PernosMesh[3].transform.position = Perno1MeshNewTransform.position;
                            PernosMesh[3].SetActive(false);
                            if (!_seUsoElBotador1 && !_seUsoElBotador2) PernosMesh[5].SetActive(true);
                            //PernosRefe[contactoV].SetActive(false);
                            if (_botadorEnHueco1 == 0)
                            {
                                /*PernosGrab[0].SetActive(true);
                                PernosGrab[0].GetComponent<NewReturn_Pos0>().enabled = false;
                                PernosGrab[0].transform.SetParent(PuntoPernoEnMaquina.transform);
                                PernosGrab[0].transform.localPosition = LocalPosPernoSpawn;
                                PernosGrab[0].transform.localEulerAngles = LocalRotPernoSpawn;
                                PernosGrab[0].GetComponent<BoxCollider>().enabled = false;
                                PernosGrab[0].GetComponent<Rigidbody>().isKinematic = true;
                                PernosGrab[0].GetComponent<Rigidbody>().useGravity = false;
                                PernoEnMano[0] = false;
                                PernoEnDado = true;
                                Boquilla_ContactoRefe = true;*/
                                Debug.Log("sacando perno y verificando el sonido" + (contactoV));
                                AudioManager.aSource.altoFxLoop("IWG_Rot01");
                                //AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                                //Arrows[14].SetActive(true);
                                PernosRefe[2].SetActive(false);
                                //_lastPernosBotadorPosition[0] = PernosMesh[3].transform;
                                _seUsoElBotador2 = true;
                                Arrows[17].SetActive(false);
                                if (_pernosBotadoresUsados == 0)
                                {
                                    MotorElectrico.position = new Vector3(-1.7815f, MotorElectrico.position.y, MotorElectrico.position.z);
                                    _pernosBotadoresUsados = 1;
                                }
                                //PernosGrab[0].GetComponent<One_Hand_PickUp>().enabled = false;
                                EjectComponent();
                                break;
                            }
                            if (_botadorEnHueco1 == 1)
                            {
                                /*PernosGrab[1].SetActive(true);
                                PernosGrab[1].GetComponent<NewReturn_Pos0>().enabled = false;
                                PernosGrab[1].transform.SetParent(PuntoPernoEnMaquina.transform);
                                PernosGrab[1].transform.localPosition = LocalPosPernoSpawn;
                                PernosGrab[1].transform.localEulerAngles = LocalRotPernoSpawn;
                                PernosGrab[1].GetComponent<BoxCollider>().enabled = false;
                                PernosGrab[1].GetComponent<Rigidbody>().isKinematic = true;
                                PernosGrab[1].GetComponent<Rigidbody>().useGravity = false;
                                PernoEnMano[1] = false;
                                PernoEnDado = true;
                                Boquilla_ContactoRefe = true;*/
                                Debug.Log("sacando perno y verificando el sonido" + (contactoV));
                                AudioManager.aSource.altoFxLoop("IWG_Rot01");
                                //AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                                //Arrows[14].SetActive(true);
                                PernosRefe[2].SetActive(false);
                                //_lastPernosBotadorPosition[1] = PernosMesh[3].transform;
                                _seUsoElBotador2 = true;
                                Arrows[17].SetActive(false);
                                if (_pernosBotadoresUsados == 0)
                                {
                                    MotorElectrico.position = new Vector3(-1.7815f, MotorElectrico.position.y, MotorElectrico.position.z);
                                    _pernosBotadoresUsados = 1;
                                }
                                //PernosGrab[1].GetComponent<One_Hand_PickUp>().enabled = false;
                                EjectComponent();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Boquilla_ContactoRefe = false;
                    //aSource.altoFx("IWG_Rot02");
                }
                break;
        }
    }

    public void CheckHooks(int tareaCompletada)
    {
        if (HookObject1.GetIsHooked() && HookObject2.GetIsHooked())
        {
            TareaCompletada(tareaCompletada);
        }
        if (HookObject1.GetIsHooked())
        {
            Arrows[4].SetActive(false);
        }
        if (HookObject2.GetIsHooked())
        {
            Arrows[5].SetActive(false);
        }
    }

    public void ObjPickedUp(bool grabbed)
    {
        ConectorMPPickedUp = grabbed;
    }

    public void Si_PernoEnMano(int NpEnMano)
    {
        PernoEnDado = false;
        PernoEnMano[NpEnMano] = true;
        NewReturn_Pos0 newReturn_Pos0Perno = PernosGrab[NpEnMano].GetComponent<NewReturn_Pos0>();
        newReturn_Pos0Perno.enabled = true;
        if (!_isPostTimeSkip)
        {
            newReturn_Pos0Perno.inGravKinec = true;
            newReturn_Pos0Perno.usarAuxPos0 = false;
            newReturn_Pos0Perno.SetTransform(PernoSacadoTransform.position, PernoSacadoTransform.eulerAngles);
        }
        else
        {
            if (!_seSeparoElMotorElectrico)
            {
                newReturn_Pos0Perno.inGravKinec = false;
                newReturn_Pos0Perno.usarAuxPos0 = true;
                if (NpEnMano == 0) newReturn_Pos0Perno.SetAuxTransform(pernoBotadorPosition.position, pernoBotadorPosition.eulerAngles);
                if (NpEnMano == 1) newReturn_Pos0Perno.SetAuxTransform(pernoBotadorPosition2.position, pernoBotadorPosition2.eulerAngles);

                if (_lastPernosBotadorPosition[NpEnMano] != null)
                {
                    newReturn_Pos0Perno.inGravKinec = true;
                    newReturn_Pos0Perno.SetAuxTransform(_lastPernosBotadorPosition[NpEnMano].position, _lastPernosBotadorPosition[NpEnMano].eulerAngles);
                }
            }
            else
            {
                newReturn_Pos0Perno.inGravKinec = true;
                newReturn_Pos0Perno.SetAuxTransform(_lastPernosBotadorPosition[NpEnMano].position, _lastPernosBotadorPosition[NpEnMano].eulerAngles);
            }
        }
        si_algun_pernoEnMano = true;
    }

    public void Si_PernoSoltado(int NpSoltado)
    {
        PernoEnMano[NpSoltado] = false;
        si_algun_pernoEnMano = false;
    }

    public void verificarNombrePerno(string nomPer, bool si_entrando)//creado el 16-07-24
    {
        AudioManager.aSource.goFx("Perno_Contacto");//************************************************************************************************SONIDO PARA CONTACTO CON EL PISO*************AGREGADO EL 02-08-24
        if (PernoEnDado == false)
        {
            for (int i = 0; i < verificacionNPernos.Length; i++)
            {
                if (nomPer == PernosGrab[i].name)
                {
                    verificacionNPernos[i] = si_entrando;

                    if (si_entrando == true)
                    {
                        if (PernosGrab[i].GetComponent<Rigidbody>().isKinematic == true)//agregado el 07-10-24
                        {
                            Debug.Log("verificamos de que perno " + i + " si este en caja a pesar de estar en el dado");
                            pernosRGBDActived(i);
                        }
                        else
                        {
                            PernosGrab[i].GetComponent<One_Hand_PickUp>().enabled = false;//desactiva este escript paa que no puede ser agarrado denuevo//creado 17/-07-24
                        }


                        PernosGrab[i].name = "Perno_" + i + "_EnCaja";

                    }
                    else
                    {
                        PernosGrab[i].GetComponent<One_Hand_PickUp>().enabled = true;//desactiva este escript paa que no puede ser agarrado denuevo//creado 17/-07-24
                    }
                    break;
                }
                else
                {
                    //Debug.Log("Nombre "+nomPer+" no coincide con PernosGrab["+i+"]");
                }
            }
        }

    }

    public void verificarNombrePernoSingular()
    {
        AudioManager.aSource.goFx("Perno_Contacto");
    }

    public void pernosRGBDActived(int nPerno)//cuando agarra el perno
    {
        Debug.Log("Perno" + (nPerno) + "agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");

        PernosGrab[nPerno].transform.SetParent(PuntoDeRecepccionPernos.transform);
        PernosGrab[nPerno].GetComponent<BoxCollider>().enabled = true;
        PernosGrab[nPerno].GetComponent<Rigidbody>().isKinematic = false;
        PernosGrab[nPerno].GetComponent<Rigidbody>().useGravity = true;
        PernoEnMano[nPerno] = false;


    }

    public void VolverAIntentar(int evento)
    {
        switch (evento)
        {
            case 0: // Se repite la tarea de: Drenar aceite
                Tablero_Indicaciones[1].SetActive(true);
                TableroFails[0].SetActive(false);
                break;
            case 1: // Se repite la tarea de: Elegir cadena
                foreach (GameObject chainVerification in ChainVerification)
                {
                    chainVerification.SetActive(false);
                }
                TablerosOther[2].SetActive(true);
                _hasCorrectChain = false;
                Tablero_Indicaciones[2].gameObject.SetActive(true);
                TableroFails[1].gameObject.SetActive(false);
                foreach (GameObject chainInterectable in ChainInterectable)
                {
                    chainInterectable.SetActive(true);
                    chainInterectable.GetComponent<One_Hand_PickUp>().enabled = false;
                    if (chainInterectable.TryGetComponent<Return_Pos0>(out Return_Pos0 return_Pos0))
                    {
                        return_Pos0.reposicionObj();
                        chainInterectable.GetComponent<Return_Pos0>().reposicionObj();
                    }
                    if (chainInterectable.TryGetComponent<NewReturn_Pos0>(out NewReturn_Pos0 newReturn_Pos0))
                    {
                        newReturn_Pos0.reposicionObj();
                        chainInterectable.GetComponent<NewReturn_Pos0>().reposicionObj();
                    }
                    chainInterectable.GetComponent<One_Hand_PickUp>().enabled = true;
                }
                ChainReference.SetActive(true);
                ComprobarCadenaConformeButton.SetActive(false);
                break;
        }
    }

    public void EventoFail(int evento)
    {
        aSource.goFx("Fallo");
        aSource.goFx("Locu_Fallo");
        switch (evento)
        {
            case 0: // Ocurre cuando: No se drenó todo el aceite.
                Tablero_Indicaciones[1].gameObject.SetActive(false);
                TableroFails[0].gameObject.SetActive(true);
                FailArrows[0].SetActive(true);
                if (!HasOilBeenDrainedFromFrenoDeServicio)
                {
                    FailArrows[2].SetActive(true);
                    FailArrows[4].SetActive(true);
                }
                if (!HasOilBeenDrainedFromSistemaDeTransmision)
                {
                    FailArrows[1].SetActive(true);
                    FailArrows[3].SetActive(true);
                }
                if (si_login == true)
                {
                    TM_Lobby.lb.auxNotas[0]++;
                }
                break;
            case 1: // Ocurre cuando: Se selecciona la cadena equivocada.
                Tablero_Indicaciones[2].gameObject.SetActive(false);
                TableroFails[1].gameObject.SetActive(true);
                ChainReference.SetActive(false);
                ComprobarCadenaConformeButton.SetActive(false);
                if (si_login == true)
                {
                    TM_Lobby.lb.auxNotas[1]++;
                }
                break;
            case 2: // Ocurre cuando: No se presiona la corneta del puente grua
                TableroFails[2].gameObject.SetActive(true);
                TableroFails[4].gameObject.SetActive(true);
                BloqueadorDeMovimientoDeMotorElectrico.SetActive(true);
                BloqueadorDeMovimientoDeMotorElectrico.transform.parent = null;
                StartCoroutine(SecondAttemptCornetaPresionada());
                if (si_login == true)
                {
                    TM_Lobby.lb.auxNotas[3]++;
                }
                break;
            case 3: // Ocurre cuando las cadenas chocan con el MT despues de quitarlas del motor electrico
                TableroFails[3].gameObject.SetActive(true);
                DetectorChoqueDeCadenas.SetActive(false);
                FloorArrows[3].SetActive(true);
                BloqueadorMTParaCadenas.SetActive(true);
                break;
            case 4: // Ocurre cuando: No se presiona la corneta del puente grua en la tarea 5, cuando debe estar de vuelta al MT.
                TableroFails[5].SetActive(true);
                BloqueadorDeMovimientoDeGanchoGrua.SetActive(true);
                BloqueadorDeMovimientoDeGanchoGrua.transform.parent = null;
                StartCoroutine(SecondAttemptCornetaPresionadaVueltaTarea5());
                break;
        }
    }

    private void EjectComponent()
    {
        if (_seUsoElBotador1 && _seUsoElBotador2)
        {
            Arrows[14].SetActive(true);
            HookObject1.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            HookObject1.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            HookObject1.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().useGravity = false;
            MotorElectrico.parent = CargaIzable;
            MotorElectrico.position = new Vector3(-1.792f, MotorElectrico.position.y, MotorElectrico.position.z);
            _pernosBotadoresUsados = 2;
            Destroy(MotorElectricoWithRigidbody.GetComponent<Rigidbody>());
            HookObject1.GetObjectToHookWith().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            HookObject2.GetObjectToHookWith().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _seSeparoElMotorElectrico = true;
            aSource.goFx("Bien");
            aSource.PlayVoz("PlaceholderLoquendo04");
            PernosMesh[4].SetActive(false);
            PernosMesh[5].SetActive(false);
            PernosGrab[0].transform.position = PernosMesh[4].transform.position;
            PernosGrab[0].transform.rotation = PernosMesh[4].transform.rotation;
            PernosGrab[0].GetComponent<NewReturn_Pos0>().SetAuxPos0(PernosGrab[0].transform.position);
            PernosGrab[1].GetComponent<NewReturn_Pos0>().SetAuxPos0(PernosGrab[1].transform.position);
            PernosGrab[1].transform.position = PernosMesh[5].transform.position;
            PernosGrab[1].transform.rotation = PernosMesh[5].transform.rotation;
            PernosGrab[0].SetActive(true);
            PernosGrab[1].SetActive(true);
            PernosGrab[0].GetComponent<Rigidbody>().isKinematic = true;
            PernosGrab[0].GetComponent<Rigidbody>().useGravity = false;
            PernoEnMano[0] = false;
            PernosGrab[1].GetComponent<Rigidbody>().isKinematic = true;
            PernosGrab[1].GetComponent<Rigidbody>().useGravity = false;
            PernoEnMano[1] = false;
            PernosGrab[0].GetComponent<One_Hand_PickUp>().enabled = true;
            PernosGrab[1].GetComponent<One_Hand_PickUp>().enabled = true;
        }
    }

    private IEnumerator TimeSkipEffect()
    {
        _isPostTimeSkip = true;        
        FadeOut();
        Arrows[22].SetActive(false);
        yield return new WaitForSeconds(fadeTiempo);
        TablerosMini[1].SetActive(false);
        TableroWins[1].SetActive(false);
        foreach (GameObject pernoTimeSkip in PernosAgarrables)
        {
            pernoTimeSkip.SetActive(true);
        }
        MuroDePernos.SetActive(true);
        PernosGrab[0].transform.position = pernoBotadorPosition.position;
        PernosGrab[0].transform.rotation = pernoBotadorPosition.rotation;
        PernosGrab[1].SetActive(true);
        Arrows[0].SetActive(true);
        Arrows[21].SetActive(true);
        PernosRefe[1].SetActive(true);
        PernosRefe[2].SetActive(true);
        for (int i = 0; i < FloorArrows.Length; i++)
        {
            FloorArrows[i].SetActive(false);
        }
        FloorArrows[8].SetActive(true);
        AudioManager.aSource.goFx("IWG_Rot01", 1, true, false);
        yield return new WaitForSeconds(0.75f);
        AudioManager.aSource.goFx("Perno_Contacto");
        AudioManager.aSource.altoFxLoop("IWG_Rot01");
        yield return new WaitForSeconds(0.25f);
        AudioManager.aSource.goFx("IWG_Rot01", 1, true, false);
        yield return new WaitForSeconds(0.75f);
        AudioManager.aSource.goFx("Perno_Contacto");
        AudioManager.aSource.altoFxLoop("IWG_Rot01");
        yield return new WaitForSeconds(0.25f);
        AudioManager.aSource.goFx("IWG_Rot01", 1, true, false);
        yield return new WaitForSeconds(0.75f);
        AudioManager.aSource.goFx("Perno_Contacto");
        AudioManager.aSource.altoFxLoop("IWG_Rot01");
        yield return new WaitForSeconds(0.25f);
        //yield return new WaitForSeconds(3f);
        //AudioManager.aSource.altoFxLoop("IWG_Rot01");
        FadeIn();
        Arrows[7].SetActive(true);
        Arrows[17].SetActive(true);
        PernosMesh[2].SetActive(false);
        aSource.PlayVoz("PlaceholderLoquendo03");
    }

    public void CheckOildDrainage()
    {
        if (HasOilBeenDrainedFromFrenoDeServicio && HasOilBeenDrainedFromSistemaDeTransmision)
        {
            TareaCompletada(1);
            if (si_login == true)
            {
                TM_Lobby.lb.AgregarNota(0, TM_Lobby.lb.auxNotas[0]);
            }
        }
        else EventoFail(0);
    }

    public void CheckCorrectChain()
    {
        TablerosOther[2].SetActive(false);
        if (_hasCorrectChain)
        {
            ActivarEvento(-1);
            if (si_login == true)
            {
                TM_Lobby.lb.AgregarNota(1, TM_Lobby.lb.auxNotas[1]);
            }
        }
        else EventoFail(1);
    }
    public void SwitchArrowOff(int arrowNumber)
    {
        Arrows[arrowNumber].SetActive(false);
    }

    public void SwitchArrowOffPostTimeSkip(int arrowNumber)
    {
        if (_isPostTimeSkip)
        {
            Arrows[arrowNumber].SetActive(false);
        }
    }

    public void SwitchArrow(int arrowNumber, bool state)
    {
        Arrows[arrowNumber].SetActive(state);
    }

    public void DeactivateChainMesh(int chainNumber)
    {
        ChainMesh[chainNumber].SetActive(false);
        ChainGrab[chainNumber + 2].SetActive(false);
    }

    private void CheckIfMotorElectricoWasDisengaged()
    {
        if (_isChainUnhooked1 && _isChainUnhooked2)
        {
            DesmontarCadenaButton.SetActive(true);
            Arrows[20].SetActive(true);
            MotorElectrico.parent = null;
            BloqueadorDeMovimientoDeMotorElectricoLlegadaABase.SetActive(false);
            BloqueadorDeMovimientoDeMotorElectrico.SetActive(false);
            AreasColisionMotorElectrico.SetActive(false);
            AreasColsionGanchoGrua.SetActive(true);
            CheckSePresionoCornetaTarea5Coroutine = StartCoroutine(CheckCornetaPresionadaPuenteGruaVueltaTarea5());
            //CargasIzables_ColVientMotorElectrico = FindObjectsByType<CargaIzable_ColViento>(FindObjectsSortMode.None);
            //CargasIzables_LimitesMotorElectrico = FindObjectsByType<CargaIzable_Limites>(FindObjectsSortMode.None);
            for (int i = 0; i < CargasIzables_ColVientMotorElectrico.Length; i++)
            {
                CargasIzables_ColVientMotorElectrico[i].ForceUnlock();
            }
            for (int i = 0; i < CargasIzables_LimitesMotorElectrico.Length; i++)
            {
                CargasIzables_LimitesMotorElectrico[i].ForceUnlock();
            }
            
        }
    }

    public void DeactivateGrilletesInTarea5()
    {
        if (TareaActual == 5)
        {
            MotorElectrico.parent = null;
            //ControlPuenteGrua.GetComponent<Control_Grua_Puente>().enabled = true;
            //BotonesControlPuenteGrua.SetActive(true);
            //GrilletesMesh[0].SetActive(false);
            //GrilletesMesh[1].SetActive(false);
        }
    }

    public void GrilleteRGBDActivate(int grilleteGrabNumber)
    {
        GrilleteGrab[grilleteGrabNumber].GetComponent<Rigidbody>().isKinematic = false;
        GrilleteGrab[grilleteGrabNumber].GetComponent<Rigidbody>().useGravity = true;
    }

    public void CadenaLockerDeactivate(int number)
    {
        //ChainInterectable[number].GetComponent<Return_Pos0>().reposicionObj();
    }

    private IEnumerator CheckCornetaPresionadaPuenteGrua()
    {
        while (!_isCornetaPresionada)
        {
            if (control_Grua_Puente.corneta_presionada)
            {
                DetectoresDeMovimientoSinCorneta.SetActive(false);
                _isCornetaPresionada = true;
                ParedParaJugadorEnMotorElectrico.SetActive(false);
                CubosBloqueParaMotorElectricoLlegada.SetActive(true);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator SecondAttemptCornetaPresionada()
    {
        while (!_isCornetaPresionada)
        {
            if (control_Grua_Puente.corneta_presionada)
            {
                TableroFails[2].SetActive(false);
                TableroFails[4].SetActive(false);
                _isCornetaPresionada = true;
                BloqueadorDeMovimientoDeMotorElectrico.SetActive(false);
                //CargasIzables_ColVientMotorElectrico = FindObjectsByType<CargaIzable_ColViento>(FindObjectsSortMode.None);
                //CargasIzables_LimitesMotorElectrico = FindObjectsByType<CargaIzable_Limites>(FindObjectsSortMode.None);
                for (int i = 0; i < CargasIzables_ColVientMotorElectrico.Length; i++)
                {
                    CargasIzables_ColVientMotorElectrico[i].ForceUnlock();
                }
                for (int i = 0; i < CargasIzables_LimitesMotorElectrico.Length; i++)
                {
                    CargasIzables_LimitesMotorElectrico[i].ForceUnlock();
                }
                ParedParaJugadorEnMotorElectrico.SetActive(false);
                CubosBloqueParaMotorElectricoLlegada.SetActive(true);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator CheckCornetaPresionadaPuenteGruaVueltaTarea5()
    {
        while (!_isCornetaPresionadaVueltaTarea5)
        {
            if (control_Grua_Puente.corneta_presionada)
            {
                DetectoresDeMovimientoSinCorneta.SetActive(false);
                _isCornetaPresionadaVueltaTarea5 = true;
                break;
            }
            yield return null;
        }
    }

    private IEnumerator SecondAttemptCornetaPresionadaVueltaTarea5()
    {
        while (!_isCornetaPresionadaVueltaTarea5)
        {
            if (control_Grua_Puente.corneta_presionada)
            {
                TableroFails[5].SetActive(false);
                DetectoresDeMovimientoSinCorneta.SetActive(false);
                _isCornetaPresionadaVueltaTarea5 = true;
                BloqueadorDeMovimientoDeGanchoGrua.SetActive(false);
                for (int i = 0; i < CargasIzables_ColVientMotorElectrico.Length; i++)
                {
                    CargasIzables_ColVientMotorElectrico[i].ForceUnlock();
                }
                for (int i = 0; i < CargasIzables_LimitesMotorElectrico.Length; i++)
                {
                    CargasIzables_LimitesMotorElectrico[i].ForceUnlock();
                }
                break;
            }
            yield return null;
        }
    }

    public void CheckCornetaPresionada()
    {
        control_Grua_Puente.corneta_presionada = true;
    }
    public void activarCtrlBTN(bool on)
    {
        if (control_Grua_Puente.BTN_CTRL.Length != 0)
        {
            for (int i = 0; i < control_Grua_Puente.BTN_CTRL.Length; i++)
            {
                ActiveObj(control_Grua_Puente.BTN_CTRL[i], on);
            }
        }
    }

    public void ActiveObj(GameObject obj, bool si)
    {
        obj.SetActive(si);
    }

    private IEnumerator DeactivateGameObjectWithDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
    private IEnumerator ActivateGameObjectAfterDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(true);
    }

    private IEnumerator SetActiveGameObjectAfterDelay(GameObject go, bool state, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(state);
    }

    private IEnumerator SetActivateGameObjectAfterIsPlayingVoz(GameObject go, bool state)
    {
        yield return new WaitForSeconds(2f);
        while (AudioManager.aSource.IsPlayingVoz() == true)
        {
            yield return new WaitForFixedUpdate();
            print(AudioManager.aSource.IsPlayingVoz());
        }
        go.SetActive(state);
    }
    private IEnumerator RepeatSoundPistolaNeumatica(float soundDuration, float silenceDuration, int times)
    {
        for (int i = 0; i < times; i++)
        {
            AudioManager.aSource.goFx("IWG_Rot01", 1, true, false);
            yield return new WaitForSeconds(soundDuration);
            AudioManager.aSource.altoFxLoop("IWG_Rot01");
            yield return new WaitForSeconds(silenceDuration);
        }
    }

    public void CheckTarea5()
    {
        if (_isChainInLocker && _isGrillete1InLocker && _isGrillete2InLocker)
        {
            LeftHandleReference.SetActive(true);
            RightHandleReference.SetActive(true);
            Arrows[18].SetActive(true);
            Arrows[19].SetActive(true);
            aSource.PlayVoz("CierreElGabinete");
        }
    }

    public void CloseLeftDoor()
    {
        _isLeftDoorClosed = true;
        LeftHandleReference.SetActive(false);
        LockerEquipmentDoors[0].transform.localRotation = Quaternion.Euler(-90, 0, -90);
        Arrows[18].SetActive(false);
        aSource.goFx("Door_Hinge", 0.75f, false, false);
        if (_isLeftDoorClosed && _isRightDoorClosed)
        {
            DetectorConclsuiones.SetActive(true);
            MuroBlockConclusiones.SetActive(false);
            FloorArrows[7].SetActive(true);
            FloorArrows[5].SetActive(false);
            for (int i = 0; i < PizarraChessFloor.Length; i++) PizarraChessFloor[i].SetActive(true);
            aSource.goFx("Bien");
            aSource.goFx("Locu_Bien");
            foreach (GameObject tableroWin in TableroWins)
            {
                tableroWin.SetActive(false);
            }
            TableroWins[4].SetActive(true);
            StartCoroutine(PlayVozWithDelay(5f, "SigaLasFlechasDelSueloParaIrAConclusiones"));
            //aSource.PlayVoz("SigaLasFlechasDelSueloParaIrAConclusiones");
        }
    }

    public void CloseRightDoor()
    {
        _isRightDoorClosed = true;
        RightHandleReference.SetActive(false);
        LockerEquipmentDoors[1].transform.localRotation = Quaternion.Euler(-90, 0, 90);
        Arrows[19].SetActive(false);
        aSource.goFx("Door_Hinge", 0.75f, false, false);
        if (_isLeftDoorClosed && _isRightDoorClosed)
        {
            DetectorConclsuiones.SetActive(true);
            MuroBlockConclusiones.SetActive(false);
            FloorArrows[7].SetActive(true);
            FloorArrows[5].SetActive(false);
            for (int i = 0; i < PizarraChessFloor.Length; i++) PizarraChessFloor[i].SetActive(true);
            aSource.goFx("Bien");
            aSource.goFx("Locu_Bien");
            foreach (GameObject tableroWin in TableroWins)
            {
                tableroWin.SetActive(false);
            }
            TableroWins[4].SetActive(true);
            StartCoroutine(PlayVozWithDelay(5f, "SigaLasFlechasDelSueloParaIrAConclusiones"));
            //aSource.PlayVoz("SigaLasFlechasDelSueloParaIrAConclusiones");
        }
    }

    private IEnumerator PlayVozWithDelay(float delay, string audioVoz)
    {
        yield return new WaitForSeconds(delay);
        aSource.PlayVoz(audioVoz);
    }

    public void TryQuitGame()
    {
        Debug.Log("Se intento salir");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DesmontarCadena()
    {
        ChainGrab[0].SetActive(false);
        ChainGrab[2].transform.parent.gameObject.SetActive(false);
        ChainGrab[2].SetActive(false);
        ChainGrab[3].SetActive(false);
        ChainInterectable[0].transform.position = DesmontarCadenaButton.transform.position;
        ChainInterectable[0].GetComponent<NewReturn_Pos0>().SetAuxPos0(ChainInterectable[0].transform.position);
        ChainInterectable[0].GetComponent<NewReturn_Pos0>().SetAuxRot0(ChainInterectable[0].GetComponent<NewReturn_Pos0>().Rot0);
        ChainInterectable[0].GetComponent<NewReturn_Pos0>().usarAuxPos0 = true;
        ChainInterectable[0].SetActive(true);
        ChainMesh[0].transform.parent.gameObject.SetActive(false);
        DesmontarCadenaButton.SetActive(false);
    }

    public void QuitarConosFinal()
    {
        BotonContinuarConclusiones.SetActive(false);
        aSource.goFx("Aplausos");
        aSource.goFx("fanfarrias");
        Muro2_Bloqueo.SetActive(false);
        Tablero_Indicaciones[7].SetActive(true);
        Tablero_Indicaciones[6].SetActive(false);
        StartCoroutine(SetActiveGameObjectAfterDelay(PizarraFinalReiniciarButton, true, 2f));
        StartCoroutine(SetActiveGameObjectAfterDelay(PizarraFinalSalirButton, true, 3f));
        aSource.PlayVoz("IzajeM3FelicidadesFinal");
        if (si_login == true)
        {
            TM_Lobby.lb.moverPanelFinal();
        }
    }
}