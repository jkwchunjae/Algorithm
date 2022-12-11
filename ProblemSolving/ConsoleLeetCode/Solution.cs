namespace ConsoleLeetCode;

public class TreeNode {
    public int val;
    public TreeNode? left;
    public TreeNode? right;
}

public class Solution
{
    public int MaxPathSum(TreeNode root)
    {
        long maxResult = int.MinValue;

        Queue<TreeNode> queue = new();
        queue.Enqueue(root);
        while (queue.Any())
        {
            var node = queue.Dequeue();
            long pathSum = node.val;
            pathSum += node.left?.SingleMax() ?? 0;
            pathSum += node.right?.SingleMax() ?? 0;

            maxResult = Math.Max(maxResult, pathSum);

            if (node.left != null)
                queue.Enqueue(node.left!);
            if (node.right != null)
                queue.Enqueue(node.right!);
        }

        return (int)maxResult;
    }
}

public static class Ex
{
    private static Dictionary<TreeNode, long> _singlemax = new();
    public static long SingleMax(this TreeNode node)
    {
        if (_singlemax.TryGetValue(node, out var m))
        {
            return m;
        }

        var leftMax = node.left?.SingleMax() ?? 0;
        var rightMax = node.right?.SingleMax() ?? 0;

        var result = node.val + Math.Max(leftMax, rightMax);
        _singlemax.Add(node, result);
        return result;
    }
}