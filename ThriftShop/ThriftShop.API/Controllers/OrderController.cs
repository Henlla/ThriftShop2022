﻿using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;
using ThriftShop.Utility;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWord;


        public OrderController(IUnitOfWork _unitOfWord)
        {
            this._unitOfWord = _unitOfWord;
        }

        [HttpGet]
        public Task<IEnumerable<Order>> GetListOrder()
        {
            return _unitOfWord.Order.GetAll(includeProperties: "orderDetails");
        }
        [HttpGet("{id}")]
        public Task<IEnumerable<Order>> GetOrderById(int id)
        {
            return _unitOfWord.Order.GetAll(filter: o => o.UserId.Equals(id));
        }

        [HttpPost]
        public async Task<Order> PostOrder(Order obj)
        {
            if (obj.CouponId == null) { obj.Coupon = null; }
            else
            {
                obj.Coupon = await _unitOfWord.Coupon.GetFirstOrDefault(filter: c => c.CouponId.Equals(obj.CouponId));
            }
            //var shopingCart= await  _unitOfWord.ShoppingCart.GetAll(filter:sc=>sc.UserId.Equals(obj.UserId));

            obj.UserInfo = await _unitOfWord.UserInfo.GetFirstOrDefault(filter: uinfo => uinfo.UserId.Equals(obj.UserId));

            //
            obj.orderDetails = null;
            obj.OrderStatus = SD.StatusPending;
            await _unitOfWord.Order.Add(obj);


            _unitOfWord.Save();
            return obj;


        }

        [HttpDelete()]
        public async Task<Order> DeleteOrder(int id)
        { 
        var model= await _unitOfWord.Order.GetFirstOrDefault(o=>o.Id.Equals(id));
            if (model != null)
            { _unitOfWord.Order.Remove(model);}
            _unitOfWord.Save();
            return model;
        }

        [HttpPut("{id}/{status}")]
        public async Task<Order> UpdateStatus(int id, string status)
        {
            Order obj = await _unitOfWord.Order.GetFirstOrDefault(o => o.Id.Equals(id));
            if (status == SD.StatusCancelled)
            {
                if (obj.OrderStatus == SD.StatusPending || obj.OrderStatus == SD.StatusApproved)
                {
                    obj.OrderStatus = SD.StatusCancelled;
                }
                else {
                    return null;
                }
            }
            else if (status == SD.StatusInProcess)
            {
                if (obj.OrderStatus == SD.StatusApproved)
                {
                    obj.OrderStatus = SD.StatusInProcess;

                }
                else {
                    return null;
                }

            }
            else if(status==SD.StatusApproved)
            {
                if (obj.OrderStatus == SD.StatusPending)
                {

                    obj.OrderStatus = SD.StatusApproved;
                }
                else
                {
                    return null;
                }
            }
            else {
                return null;
            }
            await _unitOfWord.Order.Update(obj);
            _unitOfWord.Save();
            return obj;
        }


     


    }
}