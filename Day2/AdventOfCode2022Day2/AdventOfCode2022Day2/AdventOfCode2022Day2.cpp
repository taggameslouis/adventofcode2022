#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <map>
#include <vector>

using namespace std;

const string P1_ROCK = "A";
const string P1_PAPER = "B";
const string P1_SCISSOR = "C";

const string P2_ROCK = "X";
const string P2_PAPER = "Y";
const string P2_SCISSOR = "Z";

std::map<string, vector<string>> resultMap =
{
    {
        P1_ROCK,
        {
            P2_SCISSOR, // R beats S
            P2_ROCK, // R draws R
            P2_PAPER, // P beats R
        }
    },
    
    {
        P1_PAPER,
        {
            P2_ROCK, // P beats R
            P2_PAPER, // P draws P
            P2_SCISSOR, // S beats P
        }
    },
     
    {
        P1_SCISSOR,
        {
            P2_PAPER, // S beats P
            P2_SCISSOR, // S draws S
            P2_ROCK, // R beats S
        }
    }
};

std::map<string, int> pointMap =
{
    { P2_ROCK, 1 },
    { P2_PAPER, 2 },
    { P2_SCISSOR, 3 }, 
};

int CalculatePoints(string them, string me);

int main(int argc, char* argv[])
{
    // Load the file
    ifstream myfile;
    myfile.open("input.txt");
    
    if ( myfile.is_open() )
    {
        string line;
        string left;
        string right;
        int points = 0;
        
        while ( myfile.good() )
        {
            getline (myfile, line);
            istringstream ss(line);

            ss >> left >> right;

            points += CalculatePoints(left, right);
        }

        cout << "Points: " << points;
    }
    else
    {
        cout << "File couldn't be opened.";
    }
    
    return 0; 
}

int CalculatePoints(string them, string me)
{
    auto pointIterator = pointMap.find(me);
    int amount = pointIterator->second;
    
    // Find the outcomes for their move
    auto outcomeIterator = resultMap.find(them);
    for(int i = 0; i < 3; ++i)
    {
        // Find the index of my choice to find out points won
        if(outcomeIterator->second[i] == me)
        {
            int resultPoints = i * 3;
            
            cout << them << " vs " << me << ". " << resultPoints << " + " << amount << endl;
            
            amount += resultPoints;
            
            break;
        }
    }

    return amount;
}
