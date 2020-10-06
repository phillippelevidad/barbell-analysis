# Barbell Analysis

This is just a pet project, a quick way to test different allocations with the Barbell investment strategy and see how it fares along the years.

I've just recently picked an interest in investing. I had previously played with Oi Warren (sort of a Brazilian version of Robin Hood, which I believe would be the closest match outside of Brazil). When I found out about the Barbell Anti-fragile Strategy, by Nassim Taleb, I was very impressed because he aparently found a way to yield higher-than-average results with close-to-zero risk.

The idea behind this project, as it is, is very simple:

* Given market data from 2008 to 2020...
* Define fixed allocation percentages for your wallet
* Define the starting amount, and distribute this amount according to the allocation strategy
* Make monthly deposits, also distributed as per the allocation strategy
* Calculate balances at the end of the run

Starting out with 100k and making monthly deposits of 5k for 12 years, results are:

* Invested 800k
* Profited 704,484
* For a total balance of 1,504,484

I'm sure I can do better by running scenarios and fine-tuning the rebalancing process. This goes in the backlog.
