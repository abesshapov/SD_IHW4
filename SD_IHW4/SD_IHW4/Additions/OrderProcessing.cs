using System.Collections.Generic;
using System.Threading;

namespace SD_IHW4 {
    public class OrdersProcessing {
        private List<long> orderIds = new List<long>();
        public void Process() {
            while (1==1) {
                orderIds = OrderManagement.GetAwaitingOrders();
                Thread.Sleep(10000);
                while (orderIds.Count != 0) {
                    OrderManagement.UpdateOrderState(orderIds[0], "done");
                    orderIds.RemoveAt(0);
                }
            }
        }

        ~OrdersProcessing() {
            foreach (var id in orderIds) {
                OrderManagement.UpdateOrderState(id, "cancelled");
            }
        }
    }
}