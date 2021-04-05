using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class GameController : MonoBehaviour
{
    Tauler tauler;
    public TextMeshProUGUI text00;
    public TextMeshProUGUI text01;
    public TextMeshProUGUI text02;
    public TextMeshProUGUI text03;
    public TextMeshProUGUI text10;
    public TextMeshProUGUI text11;
    public TextMeshProUGUI text12;
    public TextMeshProUGUI text13;
    public TextMeshProUGUI text20;
    public TextMeshProUGUI text21;
    public TextMeshProUGUI text22;
    public TextMeshProUGUI text23;
    public TextMeshProUGUI text30;
    public TextMeshProUGUI text31; 
    public TextMeshProUGUI text32;
    public TextMeshProUGUI text33;

    public TextMeshProUGUI score;
    public TextMeshProUGUI movements;
    public TextMeshProUGUI depthUI;

    public Button start;
    public Button Stop;
    public Button Clear;
    public Button SetDepth;
    public TMP_InputField depthinput;

    tree arbre;
    int mov = 0;
    internal int st = 0, sp = 1, sc = 0, sd = 0;

    internal int depth = 4;
    void showValues()
    {
        if (tauler.m_tauler[0][0] > 10000)
        {
            tauler.m_tauler[0][0] -= 10000;
            text00.text = tauler.m_tauler[0][0].ToString();
            tauler.m_tauler[0][0] += 10000;
        }else
            text00.text = tauler.m_tauler[0][0].ToString();

        text01.text = tauler.m_tauler[0][1].ToString();
        text02.text = tauler.m_tauler[0][2].ToString();
        text03.text = tauler.m_tauler[0][3].ToString();
        text10.text = tauler.m_tauler[1][0].ToString();
        text11.text = tauler.m_tauler[1][1].ToString();
        text12.text = tauler.m_tauler[1][2].ToString();
        text13.text = tauler.m_tauler[1][3].ToString();
        text20.text = tauler.m_tauler[2][0].ToString();
        text21.text = tauler.m_tauler[2][1].ToString();
        text22.text = tauler.m_tauler[2][2].ToString();
        text23.text = tauler.m_tauler[2][3].ToString();
        text30.text = tauler.m_tauler[3][0].ToString();
        text31.text = tauler.m_tauler[3][1].ToString();
        text32.text = tauler.m_tauler[3][2].ToString();
        text33.text = tauler.m_tauler[3][3].ToString();

        score.text = tauler.score.ToString();
        movements.text = mov.ToString();
        mov++;
    }

    // Start is called before the first frame update
    void Start()
    {
        tauler = new Tauler();
        tauler.insertValue();
        
        arbre = new tree(tauler, depth);
        //InvokeRepeating("ia", 0.01f, 0.01f);
        //ia();
        showValues();
    }

    void ia()
    {
        
        arbre.construirArbre();
        tauler = arbre.nextdir();
        showValues();

    }
    // Update is called once per frame
    void Update()
    {
        //int st = 0, sp = 1, sc = 0, sd = 0;

        if (Input.GetKeyDown("down"))
            tauler.updateTauler(2);
        else if (Input.GetKeyDown("up"))
            tauler.updateTauler(1);
        else if (Input.GetKeyDown("right"))
            tauler.updateTauler(3);
        else if (Input.GetKeyDown("left"))
            tauler.updateTauler(4);

        if (st == 1)
        {
            if (tauler.m_tauler[0][0] < 10000)
                ia();
            else
                Time.timeScale = 0;
        }
        
    }
}

public class Tauler
{
    public float[][] m_tauler;
    public float score;
    public Tauler()
    {
        m_tauler = new float[4][];
        for (int i = 0; i < 4; i++)
        {
            m_tauler[i] = new float[4];
            for (int j = 0; j < 4; j++)
                m_tauler[i][j] = 0;
        }
        score = 0;
    }

    public Tauler(Tauler t)
    {
        m_tauler = new float[4][];
        for (int i = 0; i < 4; i++)
        {
            m_tauler[i] = new float[4];
            for (int j = 0; j < 4; j++)
                m_tauler[i][j] = t.m_tauler[i][j];
        }
        score = t.score;
    }
    public void updateTauler(int dir)
    {
        float[][] aux2;
        aux2 = new float[4][];
        for (int i = 0; i < 4; i++)
        {
            aux2[i] = new float[4];
            for (int j = 0; j < 4; j++)
                aux2[i][j] = m_tauler[i][j];
        }
        int xo = 0, xf = 4;
        float[][] aux;
        switch (dir)
        {
            case 1: //AMUNT

                aux = new float[4][];
                for (int i = 0; i < 4; i++)
                {
                    aux[i] = new float[4];
                    for (int j = 0; j < 4; j++)
                        aux[i][j] = m_tauler[j][i];
                }

                for (int i = xo; i < xf; i++)
                {
                    int t = 0;
                    int d = 1;
                    int count = 0;
                    int counto = 0;

                    while (d >= 0 && d <= 3)
                    {
                        if (aux[i][d] == aux[i][t])
                        {
                            if (aux[i][d] != 0)
                            {
                                aux[i][t] *= 2;
                                score += aux[i][t];
                                for (int j = d; j < 3; j++)
                                    aux[i][j] = aux[i][j + 1];
                                aux[i][3] = 0;
                                t++;
                                d++;
                            }
                            else
                            {
                                if (counto < 1 && t < 2 && d < 2)
                                {
                                    aux[i][t] = aux[i][t + 2];
                                    aux[i][t + 2] = 0;
                                    aux[i][d] = aux[i][d + 2];
                                    aux[i][d + 2] = 0;
                                    counto++;
                                }
                                else
                                {
                                    d = -1;
                                }
                            }
                        }
                        else
                        {
                            if (aux[i][t] == 0)
                            {
                                for (int j = t; j < 3; j++)
                                    aux[i][j] = aux[i][j + 1];
                                aux[i][3] = 0;
                            }
                            else
                            {
                                if (aux[i][d] == 0)
                                {
                                    for (int j = d; j < 3; j++)
                                        aux[i][j] = aux[i][j + 1];
                                    count++;
                                    aux[i][3] = 0;
                                    if (count > 2)
                                    {
                                        t++;
                                        d++;
                                    }
                                }
                                else
                                {
                                    t++;
                                    d++;
                                }
                            }
                        }
                    }

                }


                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        m_tauler[i][j] = aux[j][i];

                break;
            case 2: //AVALL
                aux = new float[4][];
                for (int i = 0; i < 4; i++)
                {
                    aux[i] = new float[4];
                    for (int j = 0; j < 4; j++)
                        aux[i][j] = m_tauler[j][i];
                }

                for (int i = xo; i < xf; i++)
                {
                    int t = 3;
                    int d = 2;
                    int count = 0;
                    int counto = 0;

                    while (d >= 0)
                    {
                        if (aux[i][d] == aux[i][t])
                        {
                            if (aux[i][d] != 0)
                            {
                                aux[i][t] *= 2;
                                score += aux[i][t];
                                for (int j = d; j >= 1; j--)
                                    aux[i][j] = aux[i][j - 1];
                                aux[i][0] = 0;
                                t--;
                                d--;
                            }
                            else
                            {
                                if (counto < 1 && t > 1 && d > 1)
                                {
                                    aux[i][t] = aux[i][t - 2];
                                    aux[i][t - 2] = 0;
                                    aux[i][d] = aux[i][d - 2];
                                    aux[i][d - 2] = 0;
                                    counto++;
                                }
                                else
                                {
                                    d = -1;
                                }
                            }
                        }
                        else
                        {
                            if (aux[i][t] == 0)
                            {
                                for (int j = t; j >= 1; j--)
                                    aux[i][j] = aux[i][j - 1];
                                aux[i][0] = 0;
                            }
                            else
                            {
                                if (aux[i][d] == 0)
                                {
                                    for (int j = d; j >= 1; j--)
                                        aux[i][j] = aux[i][j - 1];
                                    count++;
                                    aux[i][0] = 0;
                                    if (count > 2)
                                    {
                                        t--;
                                        d--;
                                    }
                                }
                                else
                                {
                                    t--;
                                    d--;
                                }
                            }
                        }
                    }

                }

                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        m_tauler[i][j] = aux[j][i];

                break;
            case 3://DRETA

                for (int i = xo; i < xf; i++)
                {
                    int t = 3;
                    int d = 2;
                    int count = 0;
                    int counto = 0;

                    while (d >= 0)
                    {
                        if (m_tauler[i][d] == m_tauler[i][t])
                        {
                            if (m_tauler[i][d] != 0)
                            {
                                m_tauler[i][t] *= 2;
                                score += m_tauler[i][t];
                                for (int j = d; j >= 1; j--)
                                    m_tauler[i][j] = m_tauler[i][j - 1];
                                m_tauler[i][0] = 0;
                                t--;
                                d--;
                            }
                            else
                            {
                                if (counto < 1 && t > 1 && d > 1)
                                {
                                    m_tauler[i][t] = m_tauler[i][t - 2];
                                    m_tauler[i][t - 2] = 0;
                                    m_tauler[i][d] = m_tauler[i][d - 2];
                                    m_tauler[i][d - 2] = 0;
                                    counto++;
                                }
                                else
                                {
                                    d = -1;
                                }
                            }
                        }
                        else
                        {
                            if (m_tauler[i][t] == 0)
                            {
                                for (int j = t; j >= 1; j--)
                                    m_tauler[i][j] = m_tauler[i][j - 1];
                                m_tauler[i][0] = 0;
                            }
                            else
                            {
                                if (m_tauler[i][d] == 0)
                                {
                                    for (int j = d; j >= 1; j--)
                                        m_tauler[i][j] = m_tauler[i][j - 1];
                                    count++;
                                    m_tauler[i][0] = 0;
                                    if (count > 2)
                                    {
                                        t--;
                                        d--;
                                    }
                                }
                                else
                                {
                                    t--;
                                    d--;
                                }
                            }
                        }
                    }

                }

                break;

            case 4://ESQUERRA
                for (int i = xo; i < xf; i++)
                {
                    int t = 0;
                    int d = 1;
                    int count = 0;
                    int counto = 0;

                    while (d >= 0 && d <= 3)
                    {
                        if (m_tauler[i][d] == m_tauler[i][t])
                        {
                            if (m_tauler[i][d] != 0)
                            {
                                m_tauler[i][t] *= 2;
                                score += m_tauler[i][t];
                                for (int j = d; j < 3; j++)
                                    m_tauler[i][j] = m_tauler[i][j + 1];
                                m_tauler[i][3] = 0;
                                t++;
                                d++;
                            }
                            else
                            {
                                if (counto < 1 && t < 2 && d < 2)
                                {
                                    m_tauler[i][t] = m_tauler[i][t + 2];
                                    m_tauler[i][t + 2] = 0;
                                    m_tauler[i][d] = m_tauler[i][d + 2];
                                    m_tauler[i][d + 2] = 0;
                                    counto++;
                                }
                                else
                                {
                                    d = -1;
                                }
                            }
                        }
                        else
                        {
                            if (m_tauler[i][t] == 0)
                            {
                                for (int j = t; j < 3; j++)
                                    m_tauler[i][j] = m_tauler[i][j + 1];
                                m_tauler[i][3] = 0;
                            }
                            else
                            {
                                if (m_tauler[i][d] == 0)
                                {
                                    for (int j = d; j < 3; j++)
                                        m_tauler[i][j] = m_tauler[i][j + 1];
                                    count++;
                                    m_tauler[i][3] = 0;
                                    if (count > 2)
                                    {
                                        t++;
                                        d++;
                                    }
                                }
                                else
                                {
                                    t++;
                                    d++;
                                }
                            }
                        }
                    }

                }

                break;
        }

        if(dir != 0)
        {
            int v = countZeros(aux2);
            if (v == 0)
            {
                score -= 500;
            }
            else
            {
                insertValue();

            }

        }

    }

    public int countZeros(float[][] t)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (m_tauler[i][j] != t[i][j])
                    return 1;

            }
        }

        return 0;
    }

    public void insertValue()
    {
        int x = Random.Range(0, 4);
        int y = Random.Range(0, 4);
        bool ok = false;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (m_tauler[i][j] == 0)
                {
                    ok = true;
                    break;
                }

        while (m_tauler[x][y] != 0 && ok)
        {
            x = Random.Range(0, 4);
            y = Random.Range(0, 4);
        }
        if (m_tauler[x][y] != 0|| !ok)
        {
            Debug.Log("can we get an f in da chat?");
            score = -800000;
        }
        else
        {
            m_tauler[x][y] = 2;
        }
    }

}

public class Node
{
    public Node pare;
    public Tauler tau;
    public float cost;
    public int direccio;
    public List<Node> fills;
    public int dirA;

    public Node(Node a, int dir)
    {
        pare = a;
        tau = new Tauler(a.tau);
        tau.updateTauler(dir);
        cost = tau.score;
        direccio = dir;
    }
    public Node(Tauler t)
    {
        pare = null;
        tau = t;
        cost = t.score;
    }

}

public class tree
{
    Node arrel;
    List<Node> nodes;
    public int depth;
    int dirA;
    public tree(Tauler t, int profunditat)
    {
        depth = profunditat;
        arrel = new Node(t);

    }
    public void construirArbre()
    {

        List<Node> llista = new List<Node>();
        //llista.Add(arrel);

        if (nodes != null)
        {

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].dirA == dirA)
                    for (int k = 1; k <= 4; k++)
                    {
                        Node nou = new Node(nodes[i], k);
                        Node aux = new Node(nodes[i], k);
                        for(int j = 0; j < depth-1; j++)
                        {
                            aux = aux.pare;
                        }
                        nou.dirA = aux.direccio;
                        
                        if (nodes[i].fills == null) nodes[i].fills = new List<Node>();
                        nodes[i].fills.Add(nou);

                        llista.Add(nou);

                    }
            }
        }
        else
        {
            llista.Add(arrel);

            for (int i = 0; i < depth; i++)
            {
                int size = llista.Count;
                for (int j = 0; j < size; j++)
                {
                    Node pare = llista[0];
                    llista.RemoveAt(0);

                    for (int k = 1; k <= 4; k++)
                    {
                        Node nou = new Node(pare, k);
                        Node aux = new Node(pare, k);
                        for (int u = 0; u < depth-1; u++)
                        {
                            if(aux.pare != null)
                                aux = aux.pare;
                        }
                        nou.dirA = aux.direccio;

                        if(pare.fills == null) pare.fills = new List<Node>();
                        pare.fills.Add(nou);
                        llista.Add(nou);
                    }
                }


            }
        }
        nodes = llista;

    }
    private Node selectNode()
    {
        int min = 0;
        for (int i = 0; i < nodes.Count;i++)
        {
            if(nodes[i].cost > nodes[min].cost)
            {
                min = i;
            }
        }

        return nodes[min];
    }
    public Tauler nextdir()
    {
        Node min = selectNode();
        Node prev = null;
        int count = 0;
        while (count < depth)
        {
            prev = min;
            min = min.pare;
            count++;
            
        }

        arrel = prev.pare;
        arrel.pare = null;
        dirA = prev.direccio;
        Tauler t = new Tauler(prev.tau);
        bool acabat = true;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (arrel.tau.m_tauler[i][j] != prev.tau.m_tauler[i][j])
                {
                    acabat = false;
                    
                }
            }
        }
        if(acabat)
            t.m_tauler[0][0] = t.m_tauler[0][0] + 10000;

        return t;// prev.direccio;
    }
}
