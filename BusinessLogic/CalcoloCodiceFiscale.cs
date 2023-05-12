using DataLayer;
namespace BusinessLogic;

public class CF : ICodiceFiscale
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
            consonanti = new string(new char[] { consonantiArray[0], consonantiArray[2], consonantiArray[3] });
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



    public static char CFCalcolaCIN(string cf)
    {
        const string validNumber0 = "0123456789LMNPQRSTUV"; // lettere per gestire i casi di Omocodia
        const string validLetter1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string validMonth2 = "ABCDEHLMPRST";
        byte[] RE = { 1, 1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 1, 0, 0, 0 }; // 1 = lettera, 0 = Numero o lettera, 2=mese
        int[] TD = { 01, 00, 05, 07, 09, 13, 15, 17, 19, 21, 02, 04, 18, 20, 11, 03, 06, 08, 12, 14, 16, 10, 22, 25, 24, 23 };

        int cin = 0;
        bool dispari = true;

        if (string.IsNullOrWhiteSpace(cf))
            throw new ArgumentException("Parametro invalido");

        cf = cf.ToUpper();

        
        
            // mi aspetto il codice fiscale completo di 15 caratteri senza il cin finale
            if (cf.Length != 15)
                throw new ArgumentException("Lunghezza invalida");
        

        for (int i = 0; i < 15; i++)
        {
            char c = cf[i];
            // verifica carattere in posizione corretta
            byte rePos = RE[i];
            int v = -1;

            if (rePos == 0) // numeri
            {
                v = validNumber0.IndexOf(c);
                if (v > 9)
                {
                    v = validLetter1.IndexOf(c);
                }
            }
            else if (rePos == 1)  // lettere
            {
                v = validLetter1.IndexOf(c);
            }
            else if (rePos == 2) // mese
            {
                if (validMonth2.IndexOf(c) >= 0)
                {
                    v = validLetter1.IndexOf(c);
                }
            }
            if (v == -1)
            {
                //se c'é una discordanza sulla posizione lettera/numero
                throw new ArgumentException($"Carattere non valido alla posizione {i + 1}, '{cf.Substring(0, i) + " < " + cf[i] + " > " + cf.Substring(i + 1)}'");
            }

            cin += dispari == true ? TD[v] : v;
            dispari = !dispari;
        }
        cin -= (cin / 26) * 26; //cin = cin - (cin / 26) * 26;
                                //Ritorna un carattere contenente il CIN
        return validLetter1[cin];
    }
   

    public string CalcolaCodiceFiscale(PersonaDataViewModel model)
    {
        string nome = CalcolaNome(model);
        string cognome = CalcolaCognome(model);
        string giornoNascita = CalcolaValoriNascita(model).Giorno.Substring(0, 2);
        string meseNascita = CalcolaValoriNascita(model).MeseNascita.ToString();
        string annoNascita = CalcolaValoriNascita(model).Anno;
        string comune =   model.Istat;
        string codiceFisc = cognome + nome + annoNascita + meseNascita + giornoNascita + comune;
        string carattereControllo = CFCalcolaCIN(codiceFisc).ToString();
        string codicefiscale = codiceFisc + carattereControllo;

        return codicefiscale;
    }

}

