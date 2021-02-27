using System;
using System.Collections;
namespace TP1
{
    class Program
    {
        static void Main(string[] args)
        {
            CompteCourant nicolasCompteCourant = new CompteCourant(0, "Nicolas", new ArrayList(), 2000F);
            CompteEpargne nicolasCompteEpargne = new CompteEpargne(0, "Nicolas", new ArrayList(),0.02F);
            CompteCourant jeremieCompteCourant = new CompteCourant(0, "Nicolas", new ArrayList(), 500);

            nicolasCompteCourant.crediter(100);
            nicolasCompteCourant.debiter(50);
            nicolasCompteCourant.crediter(20);
            nicolasCompteCourant.debiter(20);
            

            nicolasCompteEpargne.crediter(20);
            nicolasCompteEpargne.crediter(100);
            nicolasCompteEpargne.debiter(20);

            jeremieCompteCourant.debiter(500);
            jeremieCompteCourant.debiter(200, nicolasCompteCourant);

            Console.WriteLine("solde compte courant de Nicolas: " + nicolasCompteCourant.solde);
            Console.WriteLine("solde compte épargne de Nicolas: " + nicolasCompteEpargne.solde);
            Console.WriteLine("solde compte courant de Jéremie: " + jeremieCompteCourant.solde);


            nicolasCompteCourant.afficher();
            nicolasCompteEpargne.afficher();


        }
    }
    class Compte
    {
        public float solde;
        public string nomProp;
        public ArrayList operations;

        public Compte(float s, string nom, ArrayList op)
        {
            solde = s;
            nomProp = nom;
            operations = op;
        }

        public virtual void crediter(float somme)
        {
            this.solde += somme;
            operations.Add("+" + somme);
        }

        public virtual void crediter(float somme, Compte compte)
        {
            this.solde += somme;
            compte.solde -= somme;
            operations.Add("+" + somme);
            compte.operations.Add("-" + somme);
        }

        public void debiter(float somme)
        {
            this.solde -= somme;
            operations.Add("-" + somme);
        }

        public void debiter(float somme, Compte compte)
        {
            this.solde -= somme;
            compte.solde += somme;
            operations.Add("-" + somme);
            compte.operations.Add("+" + somme);
        } 

        public virtual void afficher()
        {
            

            foreach(string op in operations)
            {
                Console.WriteLine(op);
            }
            Console.WriteLine("**********************************************");
        }
    }
    class CompteCourant : Compte
    {
        float decouvert;
        //ArrayList operations;
        public CompteCourant(float solde, string nomProp, ArrayList op, float decouvert) : base(solde, nomProp, op)
        {
            this.decouvert = decouvert;
            //this.operations = operations;
        }
        public override void afficher()
        {
            Console.WriteLine("Résumé du compte de " + base.nomProp);
            Console.WriteLine("**********************************************");
            Console.WriteLine("Compte Courant de" + base.nomProp);
            Console.WriteLine("Solde: " + base.solde);
            Console.WriteLine("Découvert autotisé: " + this.decouvert);
            Console.WriteLine();
            Console.WriteLine("Opérations: ");
            base.afficher();

        }
    }

    class CompteEpargne: Compte
    {
        float taux;
        public CompteEpargne(float solde, string nomProp, ArrayList op, float taux) : base(solde, nomProp, op)
        {
            this.taux = taux;
        }
        public override void afficher()
        {
            Console.WriteLine("Résumé du compte de " + base.nomProp);
            Console.WriteLine("**********************************************");
            Console.WriteLine("Compte Courant de" + base.nomProp);
            Console.WriteLine("Solde: " + base.solde);
            Console.WriteLine("Taux: " + this.taux);
            Console.WriteLine();
            Console.WriteLine("Opérations: ");
            base.afficher();

        }

        public override void crediter(float somme)
        {
            base.solde += somme + somme * taux;
            base.operations.Add("+" + somme);
        }
    }
}
