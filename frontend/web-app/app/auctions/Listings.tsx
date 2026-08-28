'use client';

import { AuctionCard } from "./AuctionCard";
import { AppPagination } from "../components/AppPagination";
import { useEffect, useState } from "react";
import { Auction } from "@/types";
import { getData } from "../actions/auctionActions";

export  const Listings = () => {
    
    const [auctions, setAuction] = useState<Auction[]>([]);
    const [pageCount, setPageCount] = useState(0);
    const [pageNumber, setPageNumber] = useState(1);

    useEffect(() => {
        getData(pageNumber).then(data =>{
            setAuction(data.results);
            setPageCount(data.pageCount);
        })
    }, [pageNumber]);

    if(auctions.length ===0) return <h3>Loading...</h3>

    return (
        <>
            <div className="grid grid-cols-4 gap-6">
                {auctions && auctions.map((auction) => (
                <AuctionCard key={auction.id} auction={auction }/>
            ))}</div>

            <div className="flex justify-center mt-4">
                <AppPagination pageChanged={setPageNumber} currentPage={pageNumber} pageCount={pageCount}/>
            </div>
        </>
    )
}
