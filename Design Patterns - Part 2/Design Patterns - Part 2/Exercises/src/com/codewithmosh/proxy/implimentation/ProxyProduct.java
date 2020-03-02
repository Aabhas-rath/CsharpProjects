package com.codewithmosh.proxy.implimentation;

import com.codewithmosh.proxy.DbContext;

public class ProxyProduct implements IProduct {
    private IProduct realProduct;
    private DbContext context;
    public ProxyProduct(IProduct realProduct, DbContext context) {
        this.realProduct = realProduct;
        this.context = context;
    }

    @Override
    public String getName() {
        return realProduct.getName();
    }

    @Override
    public void setName(String name) {
        realProduct.setName(name);
        context.markAsChanged(realProduct);
    }
}
