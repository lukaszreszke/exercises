namespace SadOrder;

    public class Order
    {
        private Guid OrderId;
        private OrderStatus status = OrderStatus.DRAFT;
        private List<Product> _products = new List<Product>();
        private ClientId _client;
        private IRebatePolicy _rebatePolicy;
        private ITaxCalculator _taxCalculator;

        public Order(Guid orderId, ClientId client, IRebatePolicy rebatePolicy, ITaxCalculator taxCalculator)
        {
            this.OrderId = orderId;
            _client = client;
            _rebatePolicy = rebatePolicy;
            _taxCalculator = taxCalculator;
        }

        public void SetRebatePolicy(IRebatePolicy rebatePolicy)
        {
            _rebatePolicy = rebatePolicy;
        }

        public void SetTaxCalc(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        public void AddProduct(Product product)
        {
            if (status != OrderStatus.DRAFT)
            {
                throw new Exception("can only add products to draft order");
            }

            _products.Add(product);
        }

        public void Submit()
        {
            if (!_products.Any() || status != OrderStatus.DRAFT)
            {
                throw new Exception($"cannot submit order with {_products.Count} products in {status} status");
            }

            status = OrderStatus.SUBMITTED;
        }

        public OrderStatus GetStatus()
        {
            return status;
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return _products.AsReadOnly();
        }

        public Money GetTotalPrice()
        {
            Money sum = Money.ZERO;

            foreach (var product in _products)
            {
                sum = sum.Add(product.GetPrice());
            }

            Money tax = _taxCalculator.calculateTax(_products);
            Money rebate = _rebatePolicy.calculateRebate(_products);
            return sum.Add(tax).Subtract(rebate);
        }
    }

    public class Validate
    {
        public static void IsTrue(bool statement)
        {
            throw new NotImplementedException();
        }
    }

    public class Currency
    {
        public string CurrencyCode { get; }

        private Currency(string currencyCode)
        {
            CurrencyCode = currencyCode;
        }

        public static Currency GetInstance(string currencyCode)
        {
            return new Currency(currencyCode);
        }

        public string GetCurrencyCode()
        {
            return CurrencyCode;
        }

        public object? GetSymbol()
        {
            throw new NotImplementedException();
        }
    }

    public interface ITaxCalculator
    {
        Money calculateTax(List<Product> products);
    }

    public interface IRebatePolicy
    {
        Money calculateRebate(List<Product> products);
    }

    public class ClientId
    {
    }

    public class Product
    {
        public virtual Guid ProductId { get; }
        public virtual Money Price { get; }

        public Product()
        {

        }

        public Product(Guid productId, Money price)
        {
            ProductId = productId;
            Price = price;
        }

        public virtual Money GetPrice()
        {
            return Price;
        }
    }

    public class OrderStatus
    {
        private readonly string _value;
        public static OrderStatus DRAFT = new OrderStatus(nameof(DRAFT));
        public static OrderStatus SUBMITTED = new OrderStatus(nameof(SUBMITTED));

        private OrderStatus(string value)
        {
            _value = value;
        }
    }