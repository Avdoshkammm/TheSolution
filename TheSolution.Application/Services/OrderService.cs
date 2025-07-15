using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Application.DTO;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;

namespace TheSolution.Application.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILogger<OrderService> logger;
        public OrderService(IUnitOfWork _uow, IMapper _mapper, ILogger<OrderService> _logger)
        {
            uow = _uow;
            mapper = _mapper;
            logger = _logger;
        }

        public async Task<IEnumerable<OrderProductDTO>> GetOrders()
        {
            IEnumerable<OrderProduct> orders = await uow.Orders.GetAllOrders();
            try
            {
                return mapper.Map<IEnumerable<OrderProductDTO>>(orders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"\nGetAllOrders service error");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userID)
        {
            IEnumerable<Order> uOrders = await uow.Orders.GetUserOrders(userID);
            try
            {
                return mapper.Map<IEnumerable<OrderDTO>>(uOrders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"\nGetUserOrders service error");
                throw;
            }
        }

        public async Task<OrderDTO> GetOrderInfo(int id)
        {
            Order order = await uow.Orders.GetOrderInfo(id);
            try
            {
                return mapper.Map<OrderDTO>(order);
            }
            catch (Exception ex)
            {
                logger.LogError("");
                return null;
            }
        }

        public async Task CreateNewOrder(string userID, int productID, int quantity, OrderDTO orderdto)
        {
            try
            {
                Order order = mapper.Map<Order>(orderdto);
                try
                {
                    await uow.Orders.CheckQuantity(productID, quantity);
                    try
                    {
                        await uow.Orders.CreateOrder(userID, quantity);
                        try
                        {
                            await uow.Orders.CreateOrderInfo(order.ID, productID);
                            try
                            {
                                await uow.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError("Ошибка на этапе финального сохранения");
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError("Ошибка на этапе добавления информации о заказе");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("Ошибка на этапе создания заказа");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError("Ошибка на этапе проверки количества продукта");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка маппинга заказа");
            }
        }
                
            ////Проверка количества продукта
            //Task<bool> checkQuantity = uow.Orders.CheckQuantity(productID, quantity);
            //if(checkQuantity.Result == false)
            //{
            //    logger.LogError("Продукта для заказа недостаточно(service error)");
            //    return null;
            //    throw new Exception("Недостаточно продукта для заказа");
            //}

            ////Создание щаписи в таблицу Order
            //Task createOrder = uow.Orders.CreateOrder(userID, quantity);
            ////Проверка на сощдание заказа
            //if(createOrder == null)
            //{
            //    logger.LogError("Ошибка при создании заказа(service error)");
            //    return null;
            //    throw new Exception("Ошибка при создании заказа");
            //}

            //Task<Order> checkCurrentOrder = uow.Orders.GetOrderInfo(createOrder.Id);
            //if (checkCurrentOrder == null)
            //{
            //    logger.LogError("Ошибка при проверке существования заказа");
            //    return null;
            //    throw new Exception("Ошибка при проверке существования заказа в сервисе");
            //}
            


            //await uow.Orders.CreateOrderInfo(checkCurrentOrder.Id, quantity);
            //try
            //{
            //    await uow.SaveChangesAsync();
            //    try
            //    {
            //        return mapper.Map<OrderDTO>(checkCurrentOrder);
            //    }
            //    catch(Exception ex)
            //    {
            //        logger.LogError(ex.Message);
            //        return null;
            //        throw new Exception("Ошибка при маппинге итоговых данных");
            //    }
            //}
            //catch(Exception ex)
            //{
            //    logger.LogError("Ошибка при сохранении информации о заказе");
            //    throw new Exception("Error add OrderInfo");
            //}

    }
}
