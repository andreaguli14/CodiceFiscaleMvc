using DataLayer;
namespace BusinessLogic;

public class CF
{
    private Dictionary<char, int> tabellaConversione;


    public class ValoriNascita
    {
        public string Giorno { get; set; }
        public char MeseNascita { get; set; }
        public string Anno { get; set; }
    }


    private string CalcolaNome(PersonaDataViewModel model)
    {

        // Prendo il nome dal modello e lo trasformo in maiuscolo
        string nome = model.FirstName.ToUpper();
        var consonanti = "";
        var vocali = "";

        // Riempio consonanti e vocali del nome passato
        foreach (var lettera in nome)
        {
            if (!"AEIOU ".Contains(lettera))
            {
                consonanti += lettera;
            }
            else
            {
                vocali += lettera;
            }
        }

        // Trasformo consonanti in array
        List<char> consonantiArray = consonanti.ToList();
        List<char> vocaliArray = vocali.ToList();
        // Se ci sono più di 3 consonanti, prendo le prime 2 e la quarta
        if (consonantiArray.Count >= 4)
        {
            consonanti = new string(new char[] { consonantiArray[0], consonantiArray[1], consonantiArray[3] });
        }
        else if (consonantiArray.Count == 3)
        {
            consonanti = new string(consonantiArray.ToArray());
        }
        else
        {
            consonanti = new string(consonantiArray.Concat(vocaliArray).Take(3).ToArray());
        }

        return consonanti;
    }

    private string CalcolaCognome(PersonaDataViewModel model)
    {
        // Prendo il cognome dal modello e lo trasformo in maiuscolo
        string cognome = model.LastName.ToUpper();
        var consonanti = "";
        var vocali = "";

        // Riempio consonanti e vocali del cognome passato
        foreach (var lettera in cognome)
        {//se lettera non contiene aeiou , aumenta consonanti. if !exp.ContenutaIn(arg)
            if (!"AEIOU ".Contains(lettera))
            {
                consonanti += lettera;
            }
            else
            {
                vocali += lettera;
            }
        }

        // Trasformo consonanti in lista di caratteri
        List<char> consonantiArray = consonanti.ToList();
        List<char> vocaliArray = vocali.ToList();

        // Se ci sono più di 3 consonanti, prendo le prime 3
        if (consonantiArray.Count >= 4)
        {
            consonanti = new string(consonantiArray.Take(3).ToArray());
        }
        // Se ci sono esattamente 2 consonanti e almeno una vocale, aggiungo la prima vocale
        else if (consonantiArray.Count == 2 && vocaliArray.Count > 0)
        {
            consonanti += vocaliArray[0];
        }
        // Se c'è solo una consonante e almeno due vocali, aggiungo le prime due vocali
        else if (consonantiArray.Count == 1 && vocaliArray.Count >= 2)
        {
            consonanti += new string(vocaliArray.Take(2).ToArray());
        }
        // Se ci sono meno di 3 consonanti e non ci sono abbastanza vocali, aggiungo X
        while (consonanti.Length < 3)
        {
            consonanti += "X";
        }

        return consonanti;
    }
    private ValoriNascita CalcolaValoriNascita(PersonaDataViewModel model)
    {
        DateTime giornonascita = model.Birthday;

        string anno = giornonascita.ToString("yy");

        string giorno = giornonascita.ToString("dd");
        if (model.Gender == 'f' || model.Gender == 'F')
        {
            int giornointero = int.Parse(giorno);
            giorno = (giornointero + 40).ToString();
        }

        int mese = giornonascita.Month;
        char mesenascita;

        switch (mese)
        {
            case 01:
                mesenascita = 'A';
                break;
            case 02:
                mesenascita = 'B';
                break;
            case 03:
                mesenascita = 'C';
                break;
            case 04:
                mesenascita = 'D';
                break;
            case 05:
                mesenascita = 'E';
                break;
            case 06:
                mesenascita = 'H';
                break;
            case 07:
                mesenascita = 'L';
                break;
            case 08:
                mesenascita = 'M';
                break;
            case 09:
                mesenascita = 'P';
                break;
            case 10:
                mesenascita = 'R';
                break;
            case 11:
                mesenascita = 'S';
                break;
            case 12:
                mesenascita = 'T';
                break;
            default:
                mesenascita = 'X';
                break;
        }

        return new ValoriNascita { Giorno = giorno, MeseNascita = mesenascita, Anno = anno };


    }


    private char CalcolaCarattereControllo(string codiceFis)
    {
        int totale = 0;
        tabellaConversione = new Dictionary<char, int>()
        {
            { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 },
            { '7', 7 }, { '8', 8 }, { '9', 9 }, { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 },
            { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 }, { 'I', 8 }, { 'J', 9 }, { 'K', 10 },
            { 'L', 11 }, { 'M', 12 }, { 'N', 13 }, { 'O', 14 }, { 'P', 15 }, { 'Q', 16 }, { 'R', 17 },
            { 'S', 18 }, { 'T', 19 }, { 'U', 20 }, { 'V', 21 }, { 'W', 22 }, { 'X', 23 }, { 'Y', 24 }, { 'Z', 25 }
        };
        for (int i = 0; i < 15; i++)
        {
            char carattere = codiceFis[i];
            int valore;
            if (i % 2 == 0)
            {
                // Raddoppia e somma il valore delle cifre di ordine pari
                valore = (tabellaConversione[carattere] * 2) % 10
                         + (tabellaConversione[carattere] * 2) / 10;
            }
            else
            { // Somma il valore delle cifre di ordine dispari
                valore = 2 * tabellaConversione[carattere];
                if (valore >= 10)
                {
                    // Se il valore è maggiore o uguale a 10, scompone il valore in cifre
                    // e somma le singole cifre tra loro
                    valore = valore / 10 + valore % 10;
                }
            }

            totale += valore;
        }
        int resto = totale % 26;
        char carattereControllo = ConvertiValoreInCarattere(resto);

        return carattereControllo;
    }
    char ConvertiValoreInCarattere(int valore)
    {
        // Converte il valore intero in un carattere corrispondente nella tabella di conversione
        foreach (var coppia in tabellaConversione)
        {
            if (coppia.Value == valore)
            {
                return coppia.Key;
            }
        }


        throw new ArgumentException("Valore non valido per il carattere di controllo.");
    }


    public string CalcolaCodiceFiscale(PersonaDataViewModel model)
    {
        ComuniContext context = new();
        string nome = CalcolaNome(model);
        string cognome = CalcolaCognome(model);
        string giornoNascita = CalcolaValoriNascita(model).Giorno.Substring(0, 2);
        string meseNascita = CalcolaValoriNascita(model).MeseNascita.ToString();
        string annoNascita = CalcolaValoriNascita(model).Anno;
        string comune = context.Comunis.FirstOrDefault(x => x.Comune.ToLower() == model.Istat.ToLower()).Codice;
        string codiceFisc = cognome + nome + annoNascita + meseNascita + giornoNascita + comune;
        string carattereControllo = CalcolaCarattereControllo(codiceFisc).ToString();
        string codicefiscale = codiceFisc + carattereControllo;

        return codicefiscale;
    }

}

