# N-Queens
## Alogrithm Description
  *The whole work of the algorithm is based on the formation of some placement of queens using the array M. The queen number that is placed is stored in some pointer variable, for example, with the name p. Depending on the situation, the pointer p moves to the right (p++) or left (p—) as shown in Figure 2 for 5 queens. So, the pointer p determines the number of the queen, which is located and the coordinate y. The value M [p] determines the x coordinate of the placed queen with the number p.

  *When the pointer value changes, the condition p = N is constantly checked, which means that the last queen is placed. If p = N and there are no overlaps between queens in the current placement, then this placement is fixed. In our case, the placement is fixed in the ListBox list as a special format string. If p<N, then the following changes of the pointer p (the number of the placed queen) or M[p] are determined depending on whether there is an overlay for the current number of queens and their placement.

  *The condition for the end of the iterative process is getting all possible options. In this case, the pointer p moves to the left to the element that follows before the first, that is, to the zero element.
