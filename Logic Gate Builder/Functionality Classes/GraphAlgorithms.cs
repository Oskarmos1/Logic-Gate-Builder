using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.Functionality_Classes
{
    public class GraphAlgorithms
    {
        public static dynamic[,] makeAdjacencyList(MyList<IGate> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (components.getLength() == 0) { 
                throw new ArgumentOutOfRangeException(nameof(components), components.getLength(), "The components list needs to have a length greater than 0.");
            }
            dynamic[,] adjList = new dynamic[components.getLength(), 2];
            for (int i = 0; i < components.getLength(); i++)
            {
                dynamic comp = components.getItem(i);
                if (comp == null)
                {
                    throw new InvalidOperationException($"The component at index {i} in the input list was null.");
                }
                adjList[i, 0] = comp.getName();
                adjList[i, 1] = new MyList<string>();

            }
            for (int i = 0; i < components.getLength(); i++)
            {
                dynamic comp = components.getItem(i);
                if (comp == null)
                {
                    throw new InvalidOperationException($"Component at index {i} in the input list was null.");
                }
                    if (comp.getGateType() != "LAMP")
                {
                    OutputNode o = comp.getOutput();
                    if (o == null)
                    {
                        throw new InvalidOperationException($"Component '{comp.getName()}' at index {i} has a null output node.");
                    }
                    string[] nextGates = o.getNextInputOwnerGate();
                    if (nextGates == null)
                    {
                        throw new InvalidOperationException($"Output node of component '{comp.getName()}' has a null value for next gates.");
                    }
                    for (int j = 0; j < nextGates.Length; j++)
                    {
                        if (nextGates[j] == null)
                        {
                            throw new InvalidOperationException($"A next gate name at index {j} for component '{comp.getName()}' was null.");
                        }

                        if (adjList[i, 1].doesContain(nextGates[j]) == false)
                        {
                            adjList[i, 1].add(nextGates[j]);
                        }
                        for (int k = 0; k < components.getLength(); k++)
                        {
                            if (adjList[k, 0] == nextGates[j])
                            {
                                if (adjList[k, 1].doesContain(adjList[i, 0]) == false)
                                {
                                    adjList[k, 1].add(adjList[i, 0]);
                                }
                            }
                        }
                    }
                }

            }
            return adjList;
        }
        public static bool isConnectedBFS(MyList<IGate> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (components.getLength() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(components), components.getLength(), "The components list needs to have a length greater than 0.");
            }
            bool isConnected = true;
            dynamic adjList;
            try
            {
                adjList = makeAdjacencyList(components);
            }
            catch (Exception e) {
                throw new InvalidOperationException("Failed to create adjacency list for BFS.", e);
            
        }

            Queue<string> toBeVisited = new Queue<string>();
            MyList<string> visistedNodes = new MyList<string>();
            toBeVisited.enQueue(adjList[0, 0]);
            while (toBeVisited.isEmpty() == false)
            {
                string currentComponent = toBeVisited.deQueue();
                visistedNodes.add(currentComponent);
                int indexOfComponent = -1;
                for (int i = 0; i < components.getLength(); i++)
                {
                    if (adjList[i, 0] == currentComponent)
                    {
                        indexOfComponent = i;
                    }
                }
                if (indexOfComponent == -1) 
                {
                    throw new InvalidOperationException($"BFS encountered a component '{currentComponent}' that was not found in the adjacency list.");
                }
                MyList<string> neighbours = adjList[indexOfComponent, 1];
                if (neighbours == null)
                {
                    throw new InvalidOperationException($"Adjacency list entry for '{currentComponent}' has neighbours list which is null.");
                }
                for (int i = 0; i < neighbours.getLength(); i++)
                {
                    if (neighbours.getItem(i) == null)
                    {
                        throw new InvalidOperationException($"The component '{currentComponent}' has a null neighbour.");
                    }
                    if (toBeVisited.doesContain(neighbours.getItem(i)) == false && visistedNodes.doesContain(neighbours.getItem(i)) == false)
                    {
                       
                        toBeVisited.enQueue(neighbours.getItem(i));
                    }
                }
            }
            if (visistedNodes.getLength() != components.getLength())
            {
                isConnected = false;
            }
            return isConnected;
        }
        //https://www.youtube.com/watch?v=cIBFEhD77b4
        public static MyList<IGate> khansAlgorithm(MyList<IGate> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (components.getLength() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(components), components.getLength(), "The components list needs to have a length greater than 0.");
            }
            MyList<IGate> sortedItems = new MyList<IGate>();
            int iterationCount = 0;
            int maxIterations = components.getLength() * 2; // Should mathematically be components.getLength() but I multiply it by 2 just to be cautious.
            while (sortedItems.getLength() < components.getLength())
            {
                iterationCount++;
                if (iterationCount > maxIterations) {
                    throw new InvalidOperationException("Khan's algorithm detected a cycle in the graph. The topological sort cannot be completed.");
                }
                for (int i = 0; i < components.getLength(); i++)
                {
                    bool existsInSortedItems = false;
                    if (components.getItem(i) == null)
                    {
                        throw new InvalidOperationException($"Component at index {i} in the input list was null.");
                    }
                    for (int j = 0; j < sortedItems.getLength(); j++)
                    {
                        if (sortedItems.getItem(j) == null)
                        {
                            throw new InvalidOperationException($"A null item was found in the sortedItems list at index {j}.");
                        }
                        if (sortedItems.getItem(j).getName() == components.getItem(i).getName())
                        {
                            existsInSortedItems = true;
                        }
                    }
                    if (components.getItem(i).getNumberOfInputs() == 0 && existsInSortedItems == false)
                    {
                        sortedItems.add(components.getItem(i));
                        if (components.getItem(i).getGateType() != "LAMP")
                        {
                            dynamic myG = components.getItem(i);
                            OutputNode output = myG.getOutput();
                            if (output == null)
                            {
                                throw new InvalidOperationException($"Component '{components.getItem(i)}' at index {i} has a null output node.");
                            }
                            string[] nextOwners = output.getNextInputOwnerGate();
                            if (nextOwners == null)
                            {
                                throw new InvalidOperationException($"Output node of component '{components.getItem(i).getName()}' has a null value for next input owners.");
                            }
                            for (int j = 0; j < components.getLength(); j++)
                            {
                                if (components.getItem(j) == null)
                                {
                                    throw new InvalidOperationException($"Component at index {j} in the input list was null.");
                                }
                                for (int k = 0; k < nextOwners.Length; k++)
                                {
                                    if (nextOwners[k] == null)
                                    {
                                        throw new InvalidOperationException($"A null owner name was found in the nextOwners list for component '{components.getItem(j).getName()}' at index {k}.");
                                    }
                                    if (components.getItem(j).getName() == nextOwners[k])
                                    {
                                        components.getItem(j).removeInput();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            for (int j = 0; j < components.getLength(); j++)
            {
                components.getItem(j).resetGate();
            }
            if (sortedItems.getLength() != components.getLength())
            {
                throw new InvalidOperationException("Topological sort cannot be completed.");
            }
            return sortedItems;
        }
        public static bool isCircuitComplete(MyList<IGate> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (components.getLength() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(components), components.getLength(), "The components list needs to have a length greater than 0.");
            }
            bool allGatesSatisfied = true;
            for (int i = 0; i < components.getLength(); i++)
            {
                dynamic comp = components.getItem(i);
                if (comp == null)
                {
                    throw new InvalidOperationException($"The component at index {i} in the input list was null.");
                }

                if (comp.getGateType() != "SWITCH")
                {

                    if (comp.allInputsUsed() == false)
                    {

                        allGatesSatisfied = false;
                    }
                }
            }

            bool isConnected;
            try
            {
                isConnected = isConnectedBFS(components);
            }
            catch (Exception e) {
                throw new ApplicationException("An error occured when checking that the circuit was connected.", e);
            }
            if (isConnected == true && allGatesSatisfied == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
