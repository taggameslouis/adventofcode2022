#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <map>
#include <vector>

using namespace std;

const string ROCK = "A";
const string PAPER = "B";
const string SCISSOR = "C";

const string WIN = "Z";
const string DRAW = "Y";
const string LOSE = "X";

std::map<string, vector<string>> resultMatrixMap =
{
    {
        ROCK,
        {
            SCISSOR, // R beats S
            ROCK, // R draws R
            PAPER, // P beats R
        }
    },
    
    {
        PAPER,
        {
            ROCK, // P beats R
            PAPER, // P draws P
            SCISSOR, // S beats P
        }
    },
     
    {
        SCISSOR,
        {
            PAPER, // S beats P
            SCISSOR, // S draws S
            ROCK, // R beats S
        }
    }
};

std::map<string, int> outcomeIndexMap =
{
    { LOSE, 0 },
    { DRAW, 1 },
    { WIN, 2 }, 
};

std::map<string, int> choicePointsMap =
{
    { ROCK, 1 },
    { PAPER, 2 },
    { SCISSOR, 3 }, 
};

int CalculatePoints(string theirChoice, string expectedOutcome);

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

int CalculatePoints(string theirChoice, string expectedOutcome)
{
    // Find which option we need to play
    auto outcomeIterator = outcomeIndexMap.find(expectedOutcome);
    auto resultIterator = resultMatrixMap.find(theirChoice);
    string myChoice = resultIterator->second[outcomeIterator->second];

    // Calculate points
    int choicePoints = choicePointsMap.find(myChoice)->second;
    int resultPoints = outcomeIterator->second * 3;
            
    cout << theirChoice << " vs " << expectedOutcome << ". " << choicePoints << " + " << resultPoints << endl;

    return choicePoints + resultPoints;
}
