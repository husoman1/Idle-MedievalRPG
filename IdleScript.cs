using UnityEngine;
using UnityEngine.UI;


public class IdleScript : MonoBehaviour
{
    //Production variables
        public double Production1Base;
        
        public double Production1Bonus;
        public double Production1Extra;
        public int productionUpgrade1Level;
        public double Production1CoinPerSecond;
    //Production Text

    //general Variables
    public double priceIncrease = 1.07;
    //General Coin
    public Text coinsText;
    public Text ClickValueText;
    public double coins;
    public double coinsClickValue;
    public Text coinsPerSecText;
    public double coinsPerSecond;

    //Income Update Texts
    public Text clickUpgrade1Text;
    public Text productionUpgrade1Text;
    
    public Text clickUpgrade2Text;
    public Text productionUpgrade2Text;

    //Click Upgrade Values
    public double clickUpgrade1Cost;
    public int clickUpgrade1Level;

    //Production Upgrade Values
    public double productionUpgrade1Cost;
    

    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;

    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;

    //Prestige System
    public Text GemsText;
    public Text GemsBoostText;
    public Text GemsToGetText;

    public double gems;
    public double gemsBoost;
    public double gemsToGet;


    public void Start()
    {
        Load();
    }

    public void Load()
    {
        coins = double.Parse(PlayerPrefs.GetString("coins","0"));
        coinsClickValue = double.Parse(PlayerPrefs.GetString("coinsClickValue","1"));
       
        productionUpgrade2Power = double.Parse(PlayerPrefs.GetString("productionUpgrade2Power","5"));
        //Costs 
        clickUpgrade1Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade1Cost","10"));
        clickUpgrade2Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade2Cost","100"));
        productionUpgrade1Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade1Cost","25"));
        productionUpgrade2Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade2Cost","250"));

        //Levels
        clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level",0);
        productionUpgrade1Level = PlayerPrefs.GetInt("productionUpgrade1Level",0);
        clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level",0);
        productionUpgrade2Level = PlayerPrefs.GetInt("productionUpgrade2Level",0);
    }

    public void GemToGet()
    {
        
    }


    public void resetProgress()
    {
        coins = 0;
        coinsClickValue = 1;
        productionUpgrade2Power = 5;
        clickUpgrade1Cost = 10;
        clickUpgrade2Cost = 100;
        productionUpgrade1Cost = 25;
        productionUpgrade2Cost = 250;
        clickUpgrade1Level = 0;
        productionUpgrade1Level = 0;
        clickUpgrade2Level = 0;
        productionUpgrade2Level = 0;
    }

    public void Save()
    {
        PlayerPrefs.SetString("coins",coins.ToString());
        PlayerPrefs.SetString("coinsClickValue",coinsClickValue.ToString());
       
        PlayerPrefs.SetString("productionUpgrade2Power",productionUpgrade2Power.ToString());

        //Costs 
        PlayerPrefs.SetString("clickUpgrade1Cost",clickUpgrade1Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade2Cost",clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade1Cost",productionUpgrade1Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Cost",productionUpgrade2Cost.ToString());

        //Levels
        PlayerPrefs.SetInt("clickUpgrade1Level",clickUpgrade1Level);
        PlayerPrefs.SetInt("productionUpgrade1Level",productionUpgrade1Level);
        PlayerPrefs.SetInt("clickUpgrade2Level",clickUpgrade2Level);
        PlayerPrefs.SetInt("productionUpgrade2Level",productionUpgrade2Level);

    }

    public void Update()
    {
        gemsToGet = (150 * System.Math.Sqrt(coins / 1e7));
        gemsBoost = (gems * 0.01) +1;

        GemsToGetText.text = "Prestige\n+" + gemsToGet.ToString("F2") + " Gems" ;
        GemsText.text = "Gems: " + gems.ToString("F2");
        GemsBoostText.text = gemsBoost.ToString("F2") + "x";

        production1Run();
        
        coinsPerSecond = Production1CoinPerSecond + (productionUpgrade2Power*productionUpgrade2Level) *gemsBoost;
        //This if for Coins Click Value Text. make number with e if bigger
        if (coinsClickValue > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coinsClickValue))));
            var mantissa = (coinsClickValue / System.Math.Pow(10,exponent));
            ClickValueText.text = "Thieving\n+"+mantissa.ToString("F2")+"e" + exponent +" Coins";

        }
        else
             ClickValueText.text = "Thieving\n+"+coinsClickValue.ToString("F2") + " Coins";
        //This if for Currently Available coin Text. make number with e if bigger. As note, we can assign this to a string.
        if (coins > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coins))));
            var mantissa = (coins / System.Math.Pow(10,exponent));
            coinsText.text = "Coins: "+mantissa.ToString("F2")+"e" + exponent;

        }
        else
             coinsText.text = "Coins: "+coins.ToString("F2") ;


        //ClickValueText.text = "Click\n+"+coinsClickValue + " Coins";
       // coinsText.text = "Coins: " + coins.ToString("F0"); 
        coinsPerSecText.text = coinsPerSecond.ToString("F2") + " coins/s "; 

        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1Cost.ToString("F0")+ " coins\nPower: +1 Click\nLevel: " + clickUpgrade1Level;
        productionUpgrade1Text.text = "Click Upgrade 1\nCost: " + productionUpgrade1Cost.ToString("F0")+ " coins\nPower: +1 coins/s\nLevel: " + productionUpgrade1Level;

        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2Cost.ToString("F0")+ " coins\nPower: +5 Click\nLevel: " + clickUpgrade2Level;

        productionUpgrade2Text.text = "Click Upgrade 2\nCost: " + productionUpgrade2Cost.ToString("F0")+ " coins\nPower: +"+(productionUpgrade2Power*gemsBoost)+" coins/s\nLevel: " + productionUpgrade2Level;

        coins += coinsPerSecond * Time.deltaTime;

        Save();
        
    }

    public void ClickToGetCoin()
    {
        coins += coinsClickValue;
    }  

    public void BuyClickUpgrade1()
    {
        if (coins >= clickUpgrade1Cost)
        {
            clickUpgrade1Level ++;
            coins -= clickUpgrade1Cost;
            clickUpgrade1Cost *=priceIncrease;
            coinsClickValue ++;
        }
        
    }

    public void BuyClickUpgrade2()
    {
        if (coins >= clickUpgrade2Cost)
        {
            clickUpgrade2Level ++;
            coins -= clickUpgrade2Cost;
            clickUpgrade2Cost *=priceIncrease;
            coinsClickValue +=5;
        }
        
    }

    public void productionBuyUpgrade2()
    {
        if (coins >= productionUpgrade2Cost)
        {
            productionUpgrade2Level ++;
            coins -= productionUpgrade2Cost;
            productionUpgrade2Cost *=priceIncrease;
        }
        
    }

    
    public void Prestige()
    {
        if (coins > 1000)
        {
            resetProgress();

            gems += gemsToGet;
         
        }
    }

    //Production1 Functions Start
    public void production1Run()
    {
        Production1CoinPerSecond = Production1Base * Production1Bonus * Production1Extra *  productionUpgrade1Level;
    }

    public void productionBuyUpgrade1()
    {
        if (coins >= productionUpgrade1Cost)
        {
            productionUpgrade1Level ++;
            coins -= productionUpgrade1Cost;
            productionUpgrade1Cost *=priceIncrease;
        }
        
    }
    //Production1 Functions End
}
